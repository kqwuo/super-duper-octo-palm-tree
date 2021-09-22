using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.External.Models
{
    public class ExternalAvailabilityFlight
    {
        public int availability { get; set; }
        public ExternalFlight flight { get; set; }
    }
}
