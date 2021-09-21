using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace super_duper_octo_palm_tree.app.models
{
    public class Order
    {
        public User User { get; set; }

        public int NbBought { get { return TicketList.Count; } }

        public List<Ticket> TicketList { get; set; }

        public double TotalBasePrice {
            get
            {
                return TicketList.Sum(ticket => ticket.BasePrice);
            }
        }

        public double TotalAdditionalPrice
        {
            get
            {
                return TicketList.Sum(ticket => ticket.AdditionalPrice);
            }
        }

        public double TotalDiscountedBasePrice
        {
            get
            {
                return TicketList.Sum(ticket => ticket.DiscountedBasePrice);
            }
        }

        public double TotalPrice {
            get {
                return TotalDiscountedBasePrice + TotalAdditionalPrice;
            }
        }

        //[JsonConverter(typeof(JsonStringEnumConverter))]
        public Currency UsedCurrency { get; set; }

        public double ExchangeRate { get; set; }

        public bool IsPaid { get; set; }
    }
}
