using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace super_duper_octo_palm_tree.app.models
{
    public class Flight
    {
        public Flight()
        {
            FlightSource = FlightSource.Internal;
        }

        public string IdFlight { get; set; }
        public double BasePrice { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Airport DeparturePlace { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Airport ArrivalPlace { get; set; }
        public int AvailableSeats { get; set; }
        public List<Order> Orders { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FlightSource FlightSource { get; set; }

        public object ExtraData { get; set; }

        public List<FlightOptions> Options { get; set;}

        public List<AdditionalField> AdditionalFields { get; set; }

        public double AdditionalLuggagePrice { get {
                var option = Options.Find(o => o.Label == "AdditionalLuggage");
                return option is not null ? option.Price : 0;
            }
        }

        //public Queue<Order> OrderQueue { get; }
    }
}
