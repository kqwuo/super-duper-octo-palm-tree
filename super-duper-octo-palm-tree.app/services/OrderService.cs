using super_duper_octo_palm_tree.app.models;
using System;

namespace super_duper_octo_palm_tree.app.services
{
    public class OrderService
    {
        private FlightService _routeService;
        public OrderService(FlightService routeService)
        {
            _routeService = routeService;
        }

        public bool OrderTicket(Order order, string idRoute)
        {
            //var isOrder = _routeService.SetOrder(order, idRoute);
            //return isOrder;
            return false;
        }
    }
}
