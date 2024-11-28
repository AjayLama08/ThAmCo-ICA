using System.Text.Json.Serialization;

namespace ThAmCo.Events
{
    public class VenueDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("suitability")]
        public string Suitability { get; set; }
    }
}