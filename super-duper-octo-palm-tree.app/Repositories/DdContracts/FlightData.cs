using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.Repositories.DdContracts
{
    public class FlightData
    {
        public string IdFlight { get; set; }
        public double BasePrice { get; set; }
        public double AdditionalLuggagePrice { get; set; }
        public string DeparturePlace { get; set; }
        public string ArrivalPlace { get; set; }
        public int AvailableSeats { get; set; }
    }
}
