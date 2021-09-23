using System.Text.Json.Serialization;

namespace super_duper_octo_palm_tree.app.models
{
    public class FlightOptions
    {
        public string FieldName { get; set; }

        public string Label { get; set; }

        public string ReturnType { get; set; }

        public object Value { get; set; }

        public double Price { get; set; }
    }
}
