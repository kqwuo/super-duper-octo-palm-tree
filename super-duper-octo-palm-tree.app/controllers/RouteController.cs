using Microsoft.AspNetCore.Mvc;
using super_duper_octo_palm_tree.app.services;
using System;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.controllers
{
    [Route( "/api/route" )]
    public class RouteController : ControllerBase
    {
        RouteService _routeService;

        public RouteController(RouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet("getAllRoutes")]
        public async Task<IActionResult> GetAllRoutes()
        {
            return Ok(_routeService.GetRoutes);
        }

        [HttpGet("getBookedRoute/{idRoute}")]
        public async Task<IActionResult> GetBookedRoute(Guid idRoute)
        {
            return Ok(_routeService.GetBookedRoute(idRoute));
        }
    }
}
