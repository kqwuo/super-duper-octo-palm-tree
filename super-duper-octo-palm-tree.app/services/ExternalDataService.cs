using super_duper_octo_palm_tree.app.models;
using super_duper_octo_palm_tree.app.models.external;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Flight>> GetDataAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
            request.Headers.Add(HttpRequestHeader.Accept.ToString(), "application/json");
            var response = await _httpClient.SendAsync(request);

            IEnumerable<ExternalFlight> externalFlights =
                await JsonSerializer.DeserializeAsync<IEnumerable<ExternalFlight>>(
                    await response.Content.ReadAsStreamAsync()
                );

            return (IEnumerable<Flight>)Task.FromResult(new List<ExternalFlight>());
            //return await MapExternalToInternalModel(externalFlights);
        }

        //public Task<IEnumerable<Flight>> MapExternalToInternalModel(IEnumerable<ExternalFlight>)
        //{

        //}
    }
}
