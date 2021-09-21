using System;
using System.Collections.Generic;
using System.Linq;
using super_duper_octo_palm_tree.app.models;

namespace super_duper_octo_palm_tree.app.services
{
    public class FlightService
    {
        private List<Flight> _flightList;

        public FlightService()
        {
            _flightList = new List<Flight>();
            _flightList.Add(new Flight() { IdFlight = Guid.Parse("bb1cd427-8591-42c2-9ee8-e163d2c333c8").ToString(), DeparturePlace = Airport.DTW, ArrivalPlace = Airport.CDG, BasePrice = 700, AvailableSeats = 700 }); ;
            _flightList.Add(new Flight() { IdFlight = Guid.Parse("bbc85e45-fca0-4e1c-8ed1-3ea88acedea8").ToString(), DeparturePlace = Airport.CDG, ArrivalPlace = Airport.JFK, BasePrice = 1000, AvailableSeats = 1000 });
            _flightList.Add(new Flight() { IdFlight = Guid.Parse("5261397c-2cbc-4d94-96dd-79c25354db09").ToString(), DeparturePlace = Airport.DTW, ArrivalPlace = Airport.JFK, BasePrice = 300, AvailableSeats = 300, AdditionalLuggagePrice = 30 });
        }

        public IEnumerable<Flight> GetFlights
        {
            get
            {
                return _flightList;
            }
        }

        public Flight GetFlight(string idFlight)
        {
            return _flightList.Find(flight => flight.IdFlight == idFlight);
        }

        public bool SetOrder(Order order, string idFlight)
        {
            try
            {
                var flightToGet = _flightList.Find(x => x.IdFlight == idFlight);

                if (flightToGet == null)
                    return false;

                //flightToGet.OrderQueue.Enqueue(order);

                if (flightToGet.AvailableSeats > 0 && flightToGet.AvailableSeats >= order.NbBought)
                {
                    bool isFamily = (order.TicketList.Count >= 3
                        && order.TicketList.Select(t => t.UserType == UserType.Adult).Count() >= 2
                        && order.TicketList.Select(t => t.UserType == UserType.Child).Count() >= 1);

                    foreach (Ticket ticket in order.TicketList)
                    {
                        ticket.BasePrice = flightToGet.BasePrice;
                        ticket.AdditionalPrice = flightToGet.AdditionalLuggagePrice * ticket.NbAdditionalLuggage;
                        ticket.BasePriceDiscount = (uint)(isFamily ? 10 : 0);
                    }
                    flightToGet.AvailableSeats -= order.NbBought;
                    flightToGet.Orders.Add(order);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
