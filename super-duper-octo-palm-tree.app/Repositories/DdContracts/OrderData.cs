using System;
using System.Collections.Generic;
using System.Linq;

namespace super_duper_octo_palm_tree.app.Repositories.DdContracts
{
    public class OrderData
    {
        public string IdFlight { get; set; }
        public string IdOrder { get; set; }
        public string Date { get; set; }
        public string User { get; set; }
        public string UsedCurrency { get; set; }
        public double ExchangeRate { get; set; }
        public bool IsPaid { get; set; }
    }
}
