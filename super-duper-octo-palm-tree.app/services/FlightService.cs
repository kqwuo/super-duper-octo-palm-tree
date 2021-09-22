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

        //public Flight GetFlight(string idFlight)
        //{
        //    return _flightList.Find(flight => flight.IdFlight == idFlight);
        //}

        public bool SetOrder(Order order, string idFlight)
        {
            try
            {
                var flightToGet = GetFlights().Find(x => x.IdFlight == idFlight);

                if (flightToGet == null)
                    return false;

                if (flightToGet.AvailableSeats > 0 && flightToGet.AvailableSeats >= order.NbBought)
                {
                    var idOrder = OrderDataRepository.CreateOrder(Mapper.OrderToOrderData(order), flightToGet.IdFlight);

                    bool isFamily = (order.TicketList.Count >= 3
                        && order.TicketList.Select(t => t.UserType == UserType.Adult).Count() >= 2
                        && order.TicketList.Select(t => t.UserType == UserType.Child).Count() >= 1);

                    foreach (Ticket ticket in order.TicketList)
                    {
                        ticket.BasePrice = flightToGet.BasePrice;
                        ticket.AdditionalPrice = flightToGet.AdditionalLuggagePrice * ticket.NbAdditionalLuggage;
                        ticket.BasePriceDiscount = (uint)(isFamily ? 10 : 0);
                        TicketDataRepository.CreateTicket(Mapper.TicketToTicketData(ticket), idOrder);
                    }

                    flightToGet.AvailableSeats -= order.NbBought;

                    FlightDataRepository.UpdateFlight(Mapper.FlightToFlightData(flightToGet));
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
