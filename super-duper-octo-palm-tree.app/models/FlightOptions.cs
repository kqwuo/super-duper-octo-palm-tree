using System.Text.Json.Serialization;

namespace super_duper_octo_palm_tree.app.models
{
    public class FlightOptions
    {
        public int Price { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OptionType OptionType { get; set; }
    }
}
