using System;
using System.Collections.Generic;

namespace super_duper_octo_palm_tree.app.models
{
    public class Route
    {
        public Guid IdRoute { get; set; }
        public double Price { get; set; }

        public Airport DeparturePlace { get; set; }

        public Airport ArrivalPlace { get; set; }

        public int AvailableSeats { get; set; }

        public List<Order> Orders { get; set; }
    }
}
