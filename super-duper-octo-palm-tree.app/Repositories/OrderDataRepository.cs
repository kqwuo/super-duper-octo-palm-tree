using super_duper_octo_palm_tree.app.Repositories.DdContracts;
using System.Collections.Generic;
using System.Linq;

namespace super_duper_octo_palm_tree.app.Repositories
{
    public static class OrderDataRepository
    {
        public static List<OrderData> OrderDatas { private get; set; }
        public static List<OrderData> GetAllOrderByFlightId(string idFlight) => OrderDatas.Where(x => x.IdFlight == idFlight).ToList();
        public static List<OrderData> GetOrders() => OrderDatas;
        public static void InitializeData()
        {
            OrderDatas = new List<OrderData>();
        }
    }
}
