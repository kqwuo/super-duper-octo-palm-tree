using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace super_duper_octo_palm_tree.app.External.Models
{
    public class ExternalFlight
    {
        public string code { get; set; }
        public string departure { get; set; }
        public string arrival { get; set; }
        public int base_price { get; set; }
        public ExternalPlane plane { get; set; }
        public int seats_booked { get; set; }

        public IEnumerable<ExternalFlightOptions> flightOptions { get; set; }
    }
}
