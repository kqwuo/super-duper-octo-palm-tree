using super_duper_octo_palm_tree.app.models;
using super_duper_octo_palm_tree.app.Repositories.DdContracts;
using System;
using System.Collections.Generic;
using System.Linq;
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
                                            new Flight
                                            {
                                                IdFlight = x.IdFlight,
                                                AdditionalLuggagePrice = x.AdditionalLuggagePrice,
                                                ArrivalPlace = Enum.Parse<Airport>(x.ArrivalPlace),
                                                DeparturePlace = Enum.Parse<Airport>(x.DeparturePlace),
                                                AvailableSeats = x.AvailableSeats,
                                                BasePrice = x.BasePrice,
                                                Orders = orders.Where(x => x.IdFlight == x.IdFlight)
                                                               .Select(x => new Order
                                                               {
                                                                   ExchangeRate = x.ExchangeRate,
                                                                   IsPaid = x.IsPaid,
                                                                   User = new User { Name = x.User },
                                                                   UsedCurrency = Enum.Parse<Currency>(x.UsedCurrency),
                                                                   TicketList = tickets.Where(x => x.IdOrder == x.IdOrder).Select(x => new Ticket
                                                                   {
                                                                       AdditionalPrice = x.AdditionalPrice,
                                                                       NbAdditionalLuggage = x.NbAdditionalLuggage,
                                                                       BasePrice = x.BasePrice,
                                                                       BasePriceDiscount = x.BasePriceDiscount,
                                                                       FirstName = x.FirstName,
                                                                       LastName = x.LastName,
                                                                       UserType = Enum.Parse<UserType>(x.UserType)
                                                                   }).ToList()
                                                               }).ToList()
                                            }
                                           ).ToList();
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
                NbAdditionalLuggage = ticket.NbAdditionalLuggage,
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
