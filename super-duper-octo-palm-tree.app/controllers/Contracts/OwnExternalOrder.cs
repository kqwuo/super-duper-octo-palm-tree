using super_duper_octo_palm_tree.app.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.controllers.Contracts
{
    public class OwnExternalOrder
    {
        public User User { get; set; }
        public List<OwnExternalTicket> TicketList { get; set; }
        public Currency UsedCurrency { get; set; }
        public bool IsPaid
        {
            get; set;
        }
    }
}
