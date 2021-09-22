using System;
using System.Collections.Generic;
using System.Linq;
using super_duper_octo_palm_tree.app.models;
using super_duper_octo_palm_tree.app.Repositories;

namespace super_duper_octo_palm_tree.app.services
{
    public class FlightService
    {

        public FlightService()
        {
            FlightDataRepository.InitializeData();
            OrderDataRepository.InitializeData();
            TicketDataRepository.InitializeData();
        }

        public List<Flight> GetFlights()
        {
            return Mapper.GetFlights();
        }

        public Flight GetFlight(string idFlight)
        {
            return GetFlights().Find(flight => flight.IdFlight == idFlight);
        }

    }
}
