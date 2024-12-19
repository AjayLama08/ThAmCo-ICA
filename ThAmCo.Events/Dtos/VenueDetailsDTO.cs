using System.Text.Json.Serialization;

namespace ThAmCo.Events.Dtos
{
    public class VenueDetailsDTO
    {
        [JsonPropertyName("date")]
        public DateTime date { get; set; }

        [JsonPropertyName("name")]
        public string VenueName { get; set; } = string.Empty;

        [JsonPropertyName("code")]
        public string VenueCode { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("costPerHour")]
        public double CostPerHour { get; set; }
    }
    }

