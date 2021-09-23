using Microsoft.AspNetCore.Mvc;
using super_duper_octo_palm_tree.app.Attributes;
using super_duper_octo_palm_tree.app.controllers.Contracts;
using super_duper_octo_palm_tree.app.models;
using super_duper_octo_palm_tree.app.services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.controllers
{

    [ApiKey]
    [Route("api/external")]
    public class ExternalController : ControllerBase
    {
        private readonly FlightService _flightService;
        private readonly OrderService _orderService;

        public ExternalController(FlightService flightService, OrderService orderService)
        {
            _flightService = flightService;
            _orderService = orderService;
        }

        [HttpGet("getAllFlights")]
        public async Task<IActionResult> GetAllFlights()
        {
            return Ok(_flightService.GetFlights().Select(x => new OwnExternalFlight
            {
                IdFlight = x.IdFlight,
                AdditionalLuggagePrice = x.AdditionalLuggagePrice,
                ArrivalPlace = Enum.GetName(x.ArrivalPlace),
                DeparturePlace = Enum.GetName(x.DeparturePlace),
                BasePrice = x.BasePrice,
                AvailableSeats = x.AvailableSeats,
            }));
        }


        [HttpPost("{idFlight}")]
        public async Task<IActionResult> Order(string idFlight, [FromBody] Order order)
        {
            return Ok(_orderService.OrderTicket(order, idFlight));
        }

    }
}
