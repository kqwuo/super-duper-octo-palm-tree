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
        public double AdditionalLuggagePrice { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Airport DeparturePlace { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Airport ArrivalPlace { get; set; }
        public int AvailableSeats { get; set; }
        public List<Order> Orders { get; set; }

        public IEnumerable<FlightOptions> FlightOptions { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FlightSource FlightSource { get; set; }

        public object ExtraData { get; set; }

        //public Queue<Order> OrderQueue { get; }
    }
}
