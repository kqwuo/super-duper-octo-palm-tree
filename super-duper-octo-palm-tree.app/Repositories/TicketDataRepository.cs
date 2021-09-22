using super_duper_octo_palm_tree.app.Repositories.DdContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.Repositories
{
    public static class TicketDataRepository
    {
        public static List<TicketData> TicketsData { private get; set; }
        public static List<TicketData> GetTickets() => TicketsData;
        public static void InitializeData()
        {
            TicketsData = new List<TicketData>();
        }

        public static void SaveTicket(TicketData ticket) => TicketsData.Add(ticket);
        public static void CreateTicket(TicketData ticket, string idOrder)
        {
            ticket.IdOrder = idOrder;
            TicketsData.Add(ticket);
        }
    }
}
