using super_duper_octo_palm_tree.app.models;
using super_duper_octo_palm_tree.app.models.external;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.services
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

        public IEnumerable<Flight> MapExternalToInternalModel(IEnumerable<ExternalAvailabilityFlight> externalAvailabilityFlights)
        {
            return externalAvailabilityFlights.Select(x => new Flight
            {
                AdditionalLuggagePrice = 0,
                ArrivalPlace = Enum.Parse<Airport>(x.flight.arrival),
                DeparturePlace = Enum.Parse<Airport>(x.flight.departure),
                BasePrice = x.flight.base_price,
                IdFlight = x.flight.code,
                Orders = new List<Order>(),
                AvailableSeats = x.availability,
                FlightOptions = x.flight.flightOptions.Select(x =>
                {
                    return new FlightOptions { OptionType = Enum.Parse<OptionType>(x.option_type), Price = x.price };
                }),
                FlightSource = FlightSource.External
            });
        }
    }
}
