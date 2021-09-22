using System.Text.Json.Serialization;

namespace super_duper_octo_palm_tree.app.External.Models
{
    public class ExternalPlane
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("total_seats")]
        public int total_seats { get; set; }
    }
}
