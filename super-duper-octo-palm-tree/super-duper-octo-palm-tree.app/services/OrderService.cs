using super_duper_octo_palm_tree.app.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.services
{
    public class OrderService
    {
        private RouteService _routeService;
        public OrderService(RouteService routeService)
        {
            _routeService = routeService;
        }

        public bool OrderTicket(Order order, Guid idRoute)
        {
            var isOrder = _routeService.SetOrder(order, idRoute);
            return isOrder;
        }
    }
}
