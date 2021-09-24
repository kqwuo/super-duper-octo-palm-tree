using super_duper_octo_palm_tree.app.External.Services;
using super_duper_octo_palm_tree.app.Hichem.Services;
using super_duper_octo_palm_tree.app.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.services
{
    public class CommonFlightService
    {
        private readonly FlightService flightservice;
        private readonly ExternalDataService externalDataService;
        private readonly HichemDataService hichemDataService;

        public CommonFlightService(FlightService flightservice, 
                                   ExternalDataService externalDataService,
                                   HichemDataService hichemDataService
         )
        {
            this.flightservice = flightservice;
            this.externalDataService = externalDataService;
            this.hichemDataService = hichemDataService;
        }

        public async Task<List<Flight>> GetFlightsAsync(string dateString)
        {
            var flight = new List<Flight>();
            DateTime date = DateTime.Parse(dateString);
            var ownFlights = flightservice.GetFlights(date);
            var externalFlights = await externalDataService.GetFlightAsync(date);
            var hichemFlights = await hichemDataService.GetFlightAsync();

            if (ownFlights.Count() > 0) flight.AddRange(ownFlights);
            if (externalFlights.Count() > 0) flight.AddRange(externalFlights);
            if (hichemFlights.Count() > 0) flight.AddRange(hichemFlights);

            return flight;
        }
    }
}
