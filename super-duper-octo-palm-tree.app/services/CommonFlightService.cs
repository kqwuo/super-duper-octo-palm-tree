﻿using super_duper_octo_palm_tree.app.models;
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

        public CommonFlightService(FlightService flightservice, ExternalDataService externalDataService)
        {
            this.flightservice = flightservice;
            this.externalDataService = externalDataService;
        }

        public async Task<List<Flight>> GetFlightsAsync()
        {
            var ownFlights = flightservice.GetFlights();
            var externalFlights = await externalDataService.GetFlightAsync();

            var returnFlights = ownFlights.ToList();
            if (externalFlights.Count() > 0) returnFlights.AddRange(externalFlights);

            return returnFlights;
        }
    }
}