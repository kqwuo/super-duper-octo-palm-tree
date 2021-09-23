using super_duper_octo_palm_tree.app.External.Models;
using super_duper_octo_palm_tree.app.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public async Task<IEnumerable<Flight>> GetFlightAsync()
        {
            var dateNow = DateTime.UtcNow.ToString("dd-MM-yyyy", DateTimeFormatInfo.InvariantInfo);
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
            foreach( Ticket ticket in order.TicketList )
            {
                ExternalTicket externalTicket = MapInternalTicketToExternalTicket(ticket, order.Flight);

                var request = new HttpRequestMessage(HttpMethod.Post, $@"https://api-6yfe7nq4sq-uc.a.run.app/book");
                request.Content = new StringContent(JsonSerializer.Serialize(externalTicket));
                var response = await _httpClient.SendAsync(request);
            }
        }

        public IEnumerable<Flight> MapExternalToInternalModel(IEnumerable<ExternalAvailabilityFlight> externalAvailabilityFlights)
        {
            return externalAvailabilityFlights.Select(externalFlight => new Flight
            {
                AdditionalLuggagePrice = 0,
                ArrivalPlace = Enum.Parse<Airport>(externalFlight.flight.arrival),
                DeparturePlace = Enum.Parse<Airport>(externalFlight.flight.departure),
                BasePrice = externalFlight.flight.base_price,
                IdFlight = externalFlight.flight.code,
                Orders = new List<Order>(),
                AvailableSeats = externalFlight.availability,
                FlightOptions = externalFlight.flight.flightOptions.Select(x =>
                {
                    return new FlightOptions { OptionType = Enum.Parse<OptionType>(x.option_type), Price = x.price };
                }),
                FlightSource = FlightSource.External,
                ExtraData = externalFlight
            });
        }

        public ExternalTicket MapInternalTicketToExternalTicket( Ticket ticket, Flight flight )
        {
            var result = new ExternalTicket()
            {
                flight = flight.ExtraData as ExternalFlight,
                date = "04-03-2022",
                payed_price = Convert.ToInt32(ticket.PaidTotal),
                customer_name = $"${ticket.LastName} ${ticket.FirstName}",
                customer_nationality = ticket.Nationality
            };

            if (ticket.Options.Count() > 0)
                result.options = new List<FlightOptions>();

            foreach( FlightOptions option in ticket.Options )
            {
                result.options.Add(option);
            }
            return result;
        }
    }
}
