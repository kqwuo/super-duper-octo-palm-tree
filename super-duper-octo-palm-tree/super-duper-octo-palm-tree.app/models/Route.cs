using System;
using System.Collections.Generic;

namespace super_duper_octo_palm_tree.app.models
{
    public class Route
    {
        public Route()
        {
            //IdRoute = Guid.NewGuid();
            Orders = new List<Order>();
            AdditionalLuggagePrice = 100;
            //OrderQueue = new Queue<Order>();
        }

        public Guid IdRoute { get; set; }
        public double BasePrice { get; set; }
        public double AdditionalLuggagePrice { get; set; }
        public Airport DeparturePlace { get; set; }
        public Airport ArrivalPlace { get; set; }
        public int AvailableSeats { get; set; }
        public List<Order> Orders { get; set; }
        //public Queue<Order> OrderQueue { get; }
    }
}
