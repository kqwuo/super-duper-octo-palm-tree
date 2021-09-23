using super_duper_octo_palm_tree.app.models;
using super_duper_octo_palm_tree.app.Repositories.DdContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.Repositories
{
    public static class Mapper
    {

        public static List<Flight> GetFlights()
        {
            var flights = FlightDataRepository.GetFlights();
            var orders = OrderDataRepository.GetOrders();
            var tickets = TicketDataRepository.GetTickets();

            return flights.Select(x =>
            {
                var flight = new Flight
                {
                    IdFlight = x.IdFlight,
                    ArrivalPlace = Enum.Parse<Airport>(x.ArrivalPlace),
                    DeparturePlace = Enum.Parse<Airport>(x.DeparturePlace),
                    AvailableSeats = x.AvailableSeats,
                    BasePrice = x.BasePrice,
                    Options = new List<FlightOptions>(),
                    Orders = orders.Where(x => x.IdFlight == x.IdFlight)
                                   .Select(x => new Order
                                   {
                                       ExchangeRate = x.ExchangeRate,
                                       IsPaid = x.IsPaid,
                                       User = new User { Name = x.User },
                                       UsedCurrency = Enum.Parse<Currency>(x.UsedCurrency),
                                       TicketList = tickets.Where(x => x.IdOrder == x.IdOrder).Select(x =>
                                       {
                                           var res = new Ticket
                                           {
                                               Options = new List<FlightOptions>(),
                                               BasePrice = x.BasePrice,
                                               BasePriceDiscount = x.BasePriceDiscount,
                                               FirstName = x.FirstName,
                                               LastName = x.LastName,
                                               UserType = Enum.Parse<UserType>(x.UserType)
                                           };
                                           res.Options.Add(new FlightOptions()
                                           {
                                               FieldName = "AdditionalLuggage",
                                               Label = "Baggages supplémentaires",
                                               Price = x.AdditionalPrice / (x.NbAdditionalLuggage == 0 ? 1 : x.NbAdditionalLuggage),
                                               Value = x.NbAdditionalLuggage,
                                               ReturnType = "number"
                                           });
                                           return res;
                                       }).ToList()
                                   }).ToList()
                };
                flight.Options.Add(new FlightOptions()
                {
                    FieldName = "AdditionalLuggage",
                    Label = "Baggages supplémentaires",
                    Price = x.AdditionalLuggagePrice,
                    Value = 0,
                    ReturnType = "number"
                });
                return flight;
            }).ToList();
        }

        public static OrderData OrderToOrderData(Order order)
        {
            return new OrderData
            {
                ExchangeRate = order.ExchangeRate,
                IsPaid = order.IsPaid,
                UsedCurrency = Enum.GetName(order.UsedCurrency),
                User = order.User.Name
            };
        }
        public static TicketData TicketToTicketData(Ticket ticket)
        {
            return new TicketData
            {
                AdditionalPrice = ticket.AdditionalPrice,
                BasePrice = ticket.BasePrice,
                UserType = Enum.GetName(ticket.UserType),
                BasePriceDiscount = ticket.BasePriceDiscount,
                FirstName = ticket.FirstName,
                LastName = ticket.LastName,
                NbAdditionalLuggage = ((JsonElement)ticket.Options.Find(o => o.FieldName == "AdditionalLuggage").Value).GetUInt32()
            };
        }

        public static FlightData FlightToFlightData(Flight flight)
        {
            return new FlightData
            {
                IdFlight = flight.IdFlight,
                DeparturePlace = Enum.GetName(flight.DeparturePlace),
                AdditionalLuggagePrice = flight.AdditionalLuggagePrice,
                ArrivalPlace = Enum.GetName(flight.ArrivalPlace),
                AvailableSeats = flight.AvailableSeats,
                BasePrice = flight.BasePrice
                
            };
        }

    }
}
