using System.Text.Json.Serialization;

namespace super_duper_octo_palm_tree.app.models.external
{
    public class ExternalFlight
    {
        [JsonPropertyName("code")]
        public string code { get; set; }
        [JsonPropertyName("departure")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExternalAirport departure { get; set; }
        [JsonPropertyName("arrival")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ExternalAirport arrival { get; set; }
        [JsonPropertyName("base_price")]
        public int base_price { get; set; }
        [JsonPropertyName("plane")]
        public ExternalPlane plane { get; set; }
        [JsonPropertyName("seats_booked")]
        public int seats_booked { get; set; }
    }
}
