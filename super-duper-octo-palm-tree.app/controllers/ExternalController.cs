using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using super_duper_octo_palm_tree.app.Attributes;
using super_duper_octo_palm_tree.app.controllers.Contracts;
using super_duper_octo_palm_tree.app.models;
using super_duper_octo_palm_tree.app.services;
using System;
using System.Collections.Generic;
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
        private readonly SharedCurrencyService _currencyService;
        private readonly string _apiKey;

        public ExternalController(FlightService flightService, 
                                  OrderService orderService, 
                                  SharedCurrencyService currencyService,
                                  IConfiguration configuration
        )
        {
            _flightService = flightService;
            _orderService = orderService;
            _currencyService = currencyService;
            _apiKey = configuration.GetValue<string>("ApiKey");
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
        public async Task<IActionResult> Order(string idFlight, [FromBody] OwnExternalOrder ownOrder)
        {
            var flight = _flightService.GetFlight(idFlight);
            var currency = _currencyService.GetCurrency(ownOrder.UsedCurrency);
            HttpContext.Request.Headers.TryGetValue("ApiKey", out var extractedApiKey);

            var bookingSource = extractedApiKey == _apiKey ? FlightSource.ABA : throw new Exception("Unknown ApiKey");
            var order = new Order
            {
                FlightSource = bookingSource,
                User = ownOrder.User,
                IsPaid = ownOrder.IsPaid,
                TicketList = ownOrder.TicketList.Select(x =>
                {
                    var ownTicket = new Ticket
                    {
                        BasePrice = flight.BasePrice,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        UserType = x.UserType,
                        Options = new List<FlightOptions>()
                    };

                    var flightOption = new FlightOptions
                    {
                        FieldName = "AdditionalLuggage",
                        Price = flight.AdditionalLuggagePrice,
                        Value = x.NbAdditionalLuggage,
                        Label = "Nombre de bagage additionnel",
                        ReturnType = "number"
                    };
                    ownTicket.Options.Add(flightOption);
                    return ownTicket;
                }).ToList(),
                ExchangeRate = currency,
                UsedCurrency = ownOrder.UsedCurrency
            };
            return Ok(_orderService.OrderTicket(order, idFlight));
        }

    }
}
