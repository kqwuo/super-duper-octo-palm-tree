using Microsoft.AspNetCore.Mvc;
using super_duper_octo_palm_tree.app.services;
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

    }
}
