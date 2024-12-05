using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThAmCo.Events.Dtos
{
    public class ReservationPostDTO
    {
        [JsonPropertyName("eventDate")]
        public DateTime EventDate { get; set; }
        [JsonPropertyName("venueCode")]
        public string VenueCode { get; set; } = string.Empty;
        [JsonPropertyName("reference")]
        public string Reference { get; set; } = string.Empty;
        [JsonPropertyName("staffId")]
        public string StaffId { get; set; } = string.Empty;
    }
}
