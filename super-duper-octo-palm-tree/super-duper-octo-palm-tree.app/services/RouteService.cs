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
            _routes.Add(new Route() { DeparturePlace = Airport.DTW, ArrivalPlace = Airport.CDG, Price = 700, AvailableSeats = 700, Orders = new List<Order>() });
            _routes.Add(new Route() { DeparturePlace = Airport.CDG, ArrivalPlace = Airport.JFK, Price = 1000, AvailableSeats = 1000, Orders = new List<Order>() });
            _routes.Add(new Route() { DeparturePlace = Airport.DTW, ArrivalPlace = Airport.JFK, Price = 300, AvailableSeats = 300, Orders = new List<Order>() });
        }

        public IEnumerable<Route> GetRoutes { get { return _routes; } }
    }
}
