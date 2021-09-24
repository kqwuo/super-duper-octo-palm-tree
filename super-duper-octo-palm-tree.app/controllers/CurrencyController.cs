using Microsoft.AspNetCore.Mvc;
using super_duper_octo_palm_tree.app.models;
using super_duper_octo_palm_tree.app.services;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.controllers
{
    [Route("/api/currency")]
    public class CurrencyController : ControllerBase
    {
        SharedCurrencyService _sharedCurrencyService;

        public CurrencyController(SharedCurrencyService sharedCurrencyService)
        {
            _sharedCurrencyService = sharedCurrencyService;
        }

        [HttpGet("{currencyType}")]
        public async Task<IActionResult> GetCurrencyRate(Currency currencyType)
        {
            return Ok(_sharedCurrencyService.GetCurrency(currencyType));
        }
    }
}
