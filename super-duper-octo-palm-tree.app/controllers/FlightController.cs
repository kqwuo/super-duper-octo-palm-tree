using Microsoft.AspNetCore.Mvc;
using super_duper_octo_palm_tree.app.models;
using super_duper_octo_palm_tree.app.services;
using System;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.controllers
{
    [Route( "/api/flight" )]
    public class FlightController : ControllerBase
    {
        FlightService _flightService;
        private readonly CommonFlightService commonFlightService;

        public FlightController(FlightService flightService, CommonFlightService commonFlightService)
        {
            _flightService = flightService;
            this.commonFlightService = commonFlightService;
        }

        [HttpGet("getAllFlights")]
        public async Task<IActionResult> GetAllFlights([FromQuery] Currency currency = Currency.EUR)
        {
            return Ok(await commonFlightService.GetFlightsAsync());
        }

        //[HttpGet("getFlight/{idRoute}")]
        //public async Task<IActionResult> GetBookedRoute(string idRoute)
        //{
        //    return Ok(_flightService.GetFlight(idRoute));
        //}

    }
}
