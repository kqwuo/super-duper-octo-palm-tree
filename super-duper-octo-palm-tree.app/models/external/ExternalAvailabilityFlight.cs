using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.models.external
{
    public class ExternalAvailabilityFlight
    {
        [JsonPropertyName("availability")]
        public int availability { get; set; }
        public ExternalFlight flight { get; set; }
    }
}
