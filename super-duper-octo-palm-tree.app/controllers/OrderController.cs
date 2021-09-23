using Microsoft.AspNetCore.Mvc;
using super_duper_octo_palm_tree.app.External.Services;
using super_duper_octo_palm_tree.app.models;
using super_duper_octo_palm_tree.app.services;
using System;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.controllers
{
    [Route("/api/order")]
    public class OrderController : ControllerBase
    {
        OrderService _orderService;
        ExternalDataService _externalDataService;

        public OrderController(OrderService orderService, ExternalDataService externalDataService)
        {
            _orderService = orderService;
            _externalDataService = externalDataService;
        }

        [HttpPost("{idRoute}")]
        public async Task<IActionResult> Order(string idRoute, [FromBody] Order order)
        {
            if (order.FlightSource == FlightSource.Internal)
                return Ok(_orderService.OrderTicket(order, idRoute));
            else
                return Ok(_externalDataService.BookFlightAsync(order));
        }

    }
}
