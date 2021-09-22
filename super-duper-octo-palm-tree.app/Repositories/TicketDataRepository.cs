using super_duper_octo_palm_tree.app.Repositories.DdContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.Repositories
{
    public static class TicketDataRepository
    {
        public static List<TicketData> TicketsDatas { private get; set; }
        public static List<TicketData> GetTickets() => TicketsDatas;
        public static void InitializeData()
        {
            TicketsDatas = new List<TicketData>();
        }
    }
}
