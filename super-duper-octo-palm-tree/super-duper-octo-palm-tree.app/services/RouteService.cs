using System;
using System.Collections.Generic;
using System.Linq;
using super_duper_octo_palm_tree.app.models;

namespace super_duper_octo_palm_tree.app.services
{
    public class RouteService
    {
        private List<Route> _routes;

        public RouteService()
        {
            _routes = new List<Route>();
            _routes.Add(new Route() { IdRoute = Guid.Parse("bb1cd427-8591-42c2-9ee8-e163d2c333c8"), DeparturePlace = Airport.DTW, ArrivalPlace = Airport.CDG, BasePrice = 700, AvailableSeats = 700 }); ;
            _routes.Add(new Route() { IdRoute = Guid.Parse("bbc85e45-fca0-4e1c-8ed1-3ea88acedea8"), DeparturePlace = Airport.CDG, ArrivalPlace = Airport.JFK, BasePrice = 1000, AvailableSeats = 1000 });
            _routes.Add(new Route() { IdRoute = Guid.Parse("5261397c-2cbc-4d94-96dd-79c25354db09"), DeparturePlace = Airport.DTW, ArrivalPlace = Airport.JFK, BasePrice = 300, AvailableSeats = 300 });
        }

        public IEnumerable<Route> GetRoutes { get { return _routes; } }

        public bool SetOrder(Order order, Guid idRoute)
        {
            try
            {
                var routeToGet = _routes.Find(x => x.IdRoute == idRoute);

                //routeToGet.OrderQueue.Enqueue(order);

                if(routeToGet.AvailableSeats > 0 && routeToGet.AvailableSeats >= order.NbBought)
                {
                    bool isFamily = (order.TicketList.Count >= 3
                        && order.TicketList.Select(t => t.UserType == UserType.Adult).Count() >= 2
                        && order.TicketList.Select(t => t.UserType == UserType.Child).Count() >= 1);

                    foreach ( Ticket ticket in order.TicketList )
                    {
                        ticket.BasePrice = routeToGet.BasePrice;
                        ticket.AdditionalPrice = routeToGet.AdditionalLuggagePrice * ticket.NbAdditionalLuggage;
                        ticket.BasePriceDiscount = (uint)(isFamily ? 10 : 0);
                    }
                    routeToGet.AvailableSeats -= order.NbBought;
                    routeToGet.Orders.Add(order);
                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;

        }
    }
}
