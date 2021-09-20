using System;
using System.Collections.Generic;
using super_duper_octo_palm_tree.app.models;

namespace super_duper_octo_palm_tree.app.services
{
    public class RouteService
    {
        private List<Route> _routes;

        public RouteService()
        {
            _routes = new List<Route>();
            _routes.Add(new Route() { IdRoute = Guid.NewGuid(), DeparturePlace = Airport.DTW, ArrivalPlace = Airport.CDG, Price = 700, AvailableSeats = 700, Orders = new List<Order>() });
            _routes.Add(new Route() { IdRoute = Guid.NewGuid(), DeparturePlace = Airport.CDG, ArrivalPlace = Airport.JFK, Price = 1000, AvailableSeats = 1000, Orders = new List<Order>() });
            _routes.Add(new Route() { IdRoute = Guid.NewGuid(), DeparturePlace = Airport.DTW, ArrivalPlace = Airport.JFK, Price = 300, AvailableSeats = 300, Orders = new List<Order>() });
        }

        public IEnumerable<Route> GetRoutes { get { return _routes; } }
        public Route GetBookedRoute(Guid idRoute) {
            return _routes.Find(flight => flight.IdRoute == idRoute);
        }

        public bool SetOrder(Order order, Guid idRoute)
        {
            try
            {
                var routeToGet = _routes.Find(x => x.IdRoute == idRoute);
               
                if(routeToGet.AvailableSeats > 0 && routeToGet.AvailableSeats >= order.NbBought)
                {
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
