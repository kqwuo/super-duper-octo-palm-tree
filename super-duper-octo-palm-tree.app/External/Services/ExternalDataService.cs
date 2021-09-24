using super_duper_octo_palm_tree.app.External.Models;
using super_duper_octo_palm_tree.app.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.External.Services
{
    public class ExternalDataService
    {
        private readonly HttpClient _httpClient;

        public ExternalDataService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Flight>> GetFlightAsync(DateTime date)
        {
            var dateNow = date.ToString("dd-MM-yyyy", DateTimeFormatInfo.InvariantInfo);
            var request = new HttpRequestMessage(HttpMethod.Get, $@"https://api-6yfe7nq4sq-uc.a.run.app/flights/{dateNow}");
            request.Headers.Add(HttpRequestHeader.Accept.ToString(), "application/json");
            var response = await _httpClient.SendAsync(request);

            IEnumerable<ExternalAvailabilityFlight> externalFlights =
                await JsonSerializer.DeserializeAsync<IEnumerable<ExternalAvailabilityFlight>>(
                    await response.Content.ReadAsStreamAsync()
                );

            foreach (var item in externalFlights)
            {
                var requestOption = new HttpRequestMessage(HttpMethod.Get, $@"https://api-6yfe7nq4sq-uc.a.run.app/available_options/{item.flight.code}");
                var responseRequest = await _httpClient.SendAsync(requestOption);

                item.flight.flightOptions = await JsonSerializer.DeserializeAsync<IEnumerable<ExternalFlightOptions>>(await responseRequest.Content.ReadAsStreamAsync());
            }

            return MapExternalToInternalModel(externalFlights);
        }

        public async Task BookFlightAsync(Order order)
        {
            ExternalFlight externalFlight = JsonSerializer.Deserialize<ExternalFlight>(order.ExtraData.ToString());
            foreach( Ticket ticket in order.TicketList )
            {
                try
                {
                    ExternalTicket externalTicket = MapInternalTicketToExternalTicket(ticket, externalFlight);

                    var content = JsonSerializer.Serialize(externalTicket);
                    var response = await _httpClient.PostAsync(
                        "https://api-6yfe7nq4sq-uc.a.run.app/book",
                        new StringContent(content, Encoding.UTF8, "application/json")
                    );
                    if (response.IsSuccessStatusCode)
                    {
                        var resString = response.Content.ReadAsStringAsync();
                        Console.WriteLine("Ok");
                    }
                }
                catch( Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public IEnumerable<Flight> MapExternalToInternalModel(IEnumerable<ExternalAvailabilityFlight> externalAvailabilityFlights)
        {
            return externalAvailabilityFlights.Select(eaFlight =>
            {
                var flight = new Flight
                {
                    ArrivalPlace = Enum.Parse<Airport>(eaFlight.flight.arrival),
                    DeparturePlace = Enum.Parse<Airport>(eaFlight.flight.departure),
                    BasePrice = eaFlight.flight.base_price,
                    IdFlight = eaFlight.flight.code,
                    Orders = new List<Order>(),
                    AvailableSeats = eaFlight.availability,
                    Options = eaFlight.flight.flightOptions.Select(x =>
                    {
                        return new FlightOptions { FieldName = x.option_type, Price = x.price, ReturnType = "bool", Value = false };
                    }).ToList(),
                    AdditionalFields = new List<AdditionalField>(),
                    FlightSource = FlightSource.External,
                    ExtraData = eaFlight.flight
                };
                flight.AdditionalFields.Add(new AdditionalField { Label = "Nationalité", FieldName = "customer_nationality", ReturnType = "string", Value = "" });
                return flight;
            });
        }

        public ExternalTicket MapInternalTicketToExternalTicket( Ticket ticket, ExternalFlight externalFlight )
        {
            var result = new ExternalTicket()
            {
                flight = externalFlight,
                date = DateTime.UtcNow.ToString("dd-MM-yyyy", DateTimeFormatInfo.InvariantInfo),
                payed_price = Convert.ToInt32(ticket.PaidTotal),
                customer_name = $"${ticket.LastName} ${ticket.FirstName}",
                booking_source = "oui"
            };

            if (ticket.Options.Count() > 0)
                result.options = new List<ExternalFlightOptions>();

            foreach( FlightOptions option in ticket.Options )
            {
                if (((JsonElement)option.Value).GetBoolean())
                {
                    ExternalFlightOptions externalOptions = new ExternalFlightOptions()
                    {
                        option_type = option.FieldName,
                        price = Convert.ToInt32(option.Price)
                    };
                    result.options.Add(externalOptions);
                }
            }

            var af = ticket.AdditionalFields.Find(af => af.FieldName == "customer_nationality");
            result.customer_nationality = ((JsonElement)af.Value).GetString();

            return result;
        }
    }
}
