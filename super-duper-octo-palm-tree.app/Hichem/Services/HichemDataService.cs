using Newtonsoft.Json.Linq;
using super_duper_octo_palm_tree.app.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.Hichem.Services
{
    public class HichemDataService
    {

        private readonly HttpClient _httpClient;

        public HichemDataService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Flight>> GetFlightAsync()
        {
            try
            {
                //var request = new HttpRequestMessage(HttpMethod.Get, $@"http://10.10.11.91:8014/api/flights");
                //request.Headers.Add(HttpRequestHeader.Accept.ToString(), "application/json");
                //request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkYmYxN2Q3Zi04NWJhLTRlMGQtYjUwZi05OTQxM2IxYzIxZmEiLCJuYW1lIjoiSm9obiBEb2UiLCJpYXQiOjE1MTYyMzkwMjIsInJvbGVzIjpbIlJPTEVfQVBJIl0sImFwcGxpY2F0aW9uSWQiOiIyYTIzZjU3OS05ZGQyLTRkNGItOTc0MS00MjM3N2IwMzMzMzYifQ.-GIblmqVaMsO1HIIyT6nbr62DE9fH3qS79dLjdbsIxk");
                //var response = await _httpClient.SendAsync(request);

                //var collection = JObject.Parse(await response.Content.ReadAsStringAsync());
                var collection = JObject.Parse("{\r\n    \"@context\": \"/api/contexts/Flight\",\r\n    \"@id\": \"/api/flights\",\r\n    \"@type\": \"hydra:Collection\",\r\n    \"hydra:member\": [\r\n        {\r\n            \"@id\": \"/api/flights/9b22ed0b-fe1c-47ee-a4dd-2140f0dea1c7\",\r\n            \"@type\": \"Flight\",\r\n            \"id\": \"9b22ed0b-fe1c-47ee-a4dd-2140f0dea1c7\",\r\n            \"departure\": {\r\n                \"@id\": \"/api/destinations/34b6ba6a-2fee-4e3e-b874-547185e8c093\",\r\n                \"@type\": \"Destination\",\r\n                \"name\": \"Detroit\",\r\n                \"tag\": \"DTW\"\r\n            },\r\n            \"arrival\": {\r\n                \"@id\": \"/api/destinations/c86845e3-f94f-4bc7-a082-0c36d6b7b115\",\r\n                \"@type\": \"Destination\",\r\n                \"name\": \"Charles de Gaulles\",\r\n                \"tag\": \"CDG\"\r\n            },\r\n            \"price\": \"700\",\r\n            \"seats\": 700,\r\n            \"isVIPAvailable\": true,\r\n            \"provider\": false\r\n        },\r\n        {\r\n            \"@id\": \"/api/flights/fa341b1c-2859-408c-911e-d0b65852ee3c\",\r\n            \"@type\": \"Flight\",\r\n            \"id\": \"fa341b1c-2859-408c-911e-d0b65852ee3c\",\r\n            \"departure\": {\r\n                \"@id\": \"/api/destinations/c86845e3-f94f-4bc7-a082-0c36d6b7b115\",\r\n                \"@type\": \"Destination\",\r\n                \"name\": \"Charles de Gaulles\",\r\n                \"tag\": \"CDG\"\r\n            },\r\n            \"arrival\": {\r\n                \"@id\": \"/api/destinations/d0512ea7-7d3a-4780-b3c1-d0d92b2e697c\",\r\n                \"@type\": \"Destination\",\r\n                \"name\": \"John Fitzgerald Kennedy\",\r\n                \"tag\": \"JFK\"\r\n            },\r\n            \"price\": \"1000\",\r\n            \"seats\": 1000,\r\n            \"isVIPAvailable\": true,\r\n            \"provider\": false\r\n        },\r\n        {\r\n            \"@id\": \"/api/flights/1a464fa1-6847-4b8e-be92-fc796fdd1cd5\",\r\n            \"@type\": \"Flight\",\r\n            \"id\": \"1a464fa1-6847-4b8e-be92-fc796fdd1cd5\",\r\n            \"departure\": {\r\n                \"@id\": \"/api/destinations/34b6ba6a-2fee-4e3e-b874-547185e8c093\",\r\n                \"@type\": \"Destination\",\r\n                \"name\": \"Detroit\",\r\n                \"tag\": \"DTW\"\r\n            },\r\n            \"arrival\": {\r\n                \"@id\": \"/api/destinations/d0512ea7-7d3a-4780-b3c1-d0d92b2e697c\",\r\n                \"@type\": \"Destination\",\r\n                \"name\": \"John Fitzgerald Kennedy\",\r\n                \"tag\": \"JFK\"\r\n            },\r\n            \"price\": \"300\",\r\n            \"seats\": 300,\r\n            \"isVIPAvailable\": true,\r\n            \"provider\": false\r\n        }\r\n    ],\r\n    \"hydra:totalItems\": 3,\r\n    \"hydra:search\": {\r\n        \"@type\": \"hydra:IriTemplate\",\r\n        \"hydra:template\": \"/api/flights{?date,includeProvider}\",\r\n        \"hydra:variableRepresentation\": \"BasicRepresentation\",\r\n        \"hydra:mapping\": [\r\n            {\r\n                \"@type\": \"IriTemplateMapping\",\r\n                \"variable\": \"date\",\r\n                \"property\": \"date\",\r\n                \"required\": false\r\n            },\r\n            {\r\n                \"@type\": \"IriTemplateMapping\",\r\n                \"variable\": \"includeProvider\",\r\n                \"property\": \"includeProvider\",\r\n                \"required\": false\r\n            }\r\n        ]\r\n    }\r\n}");

                return collection["hydra:member"].Select(x =>
                {
                    var flight = new Flight
                    {
                        IdFlight = x["id"].Value<string>(),
                        ArrivalPlace = Enum.Parse<Airport>(x["departure"]["tag"].Value<string>()),
                        DeparturePlace = Enum.Parse<Airport>(x["arrival"]["tag"].Value<string>()),
                        BasePrice = x["price"].Value<int>(),
                        FlightSource = FlightSource.Hichem,
                        AvailableSeats = x["seats"].Value<int>(),
                        Options = new List<FlightOptions>()

                    };

                    var hasVIP = x["isVIPAvailable"].Value<bool>();
                    if (hasVIP)
                    {
                        flight.Options.Add(new FlightOptions
                        {
                            FieldName = "isVIPAvailable",
                            Label = "VIP",
                            Price = 0,
                            ReturnType = "bool",
                            Value = false
                        });
                    }
                    return flight;
                }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
