using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace super_duper_octo_palm_tree.app.models
{
    public class Flight
    {
        public Flight()
        {
            //IdFlight = Guid.NewGuid();
            Orders = new List<Order>();
            AdditionalLuggagePrice = 100;
            //OrderQueue = new Queue<Order>();
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

        //public Queue<Order> OrderQueue { get; }
    }
}
