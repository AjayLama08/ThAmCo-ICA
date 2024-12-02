using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThAmCo.Events
{
    public class AvailabilityDTO
    {
        [JsonPropertyName("date")]
        public DateTime dateTime { get; set; }

        [JsonPropertyName("name")]
        public string venueName { get; set; } = string.Empty;

        [JsonPropertyName("code")]
        public string VenueCode { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string description { get; set;} = string.Empty;

        [JsonPropertyName("capacity")]
        public int capacity { get; set; }

        [JsonPropertyName("costPerHour")]
        public double CostPerHour { get; set; }
    }
}
