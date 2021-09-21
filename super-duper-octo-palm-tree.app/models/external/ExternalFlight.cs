namespace super_duper_octo_palm_tree.app.models.external
{
    public class ExternalFlight
    {
        public string code { get; set; }
        public ExternalAirport departure { get; set; }
        public ExternalAirport arrival { get; set; }
        public int base_price { get; set; }
        public ExternalPlane plane { get; set; }
        public int seats_booked { get; set; }
    }
}
