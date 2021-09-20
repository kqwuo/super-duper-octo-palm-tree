using Microsoft.AspNetCore.Mvc;
using super_duper_octo_palm_tree.app.models;
using super_duper_octo_palm_tree.app.services;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.controllers
{
    [Route( "/api/flight" )]
    public class FlightController : ControllerBase
    {
        FlightService _flightService;

        public FlightController(FlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet("getAllFlights")]
        public async Task<IActionResult> GetAllFlights([FromQuery] Currency currency = Currency.EUR)
        {
            return Ok(_flightService.GetFlights);
        }

    }
}
