using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.models
{
    public class Order
    {
        public User User { get; set; }

        public int NbBought { get { return TicketList.Count; } }

        public List<Ticket> TicketList { get; set; }

        public double TotalPrice {
            get {
                return TicketList.Sum(ticket => ticket.PaidTotal);
            }
        }
    }
}
