using super_duper_octo_palm_tree.app.models;
using System.Collections.Generic;

namespace super_duper_octo_palm_tree.app.External.Models
{
    public class ExternalTicket
    {
        public string code { get; set; }
        public ExternalFlight flight { get; set; }
        public string date { get; set; }
        public int payed_price { get; set; }
        public string customer_name { get; set; }
        public string customer_nationality { get; set; }
        public List<FlightOptions>? options { get; set; }
    }
}
