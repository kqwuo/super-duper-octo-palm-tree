using System;
using System.Collections.Generic;
using super_duper_octo_palm_tree.app.models;
using super_duper_octo_palm_tree.app.Repositories;

namespace super_duper_octo_palm_tree.app.services
{
    public class FlightService
    {

        public FlightService()
        {
            FlightDataRepository.InitializeRepository();
            OrderDataRepository.InitializeData();
            TicketDataRepository.InitializeData();
        }

        public List<Flight> GetFlights(DateTime date)
        {
            return Mapper.GetFlights(date);
        }

        public Flight GetFlight(string idFlight)
        {
            return Mapper.FindFlight(idFlight);
        }

    }
}
