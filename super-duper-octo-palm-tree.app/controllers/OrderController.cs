using Microsoft.AspNetCore.Mvc;
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

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("{idRoute}")]
        public async Task<IActionResult> Order(string idRoute, [FromBody] Order order)
        {
            return Ok(_orderService.OrderTicket(order, idRoute));
        }

    }
}
