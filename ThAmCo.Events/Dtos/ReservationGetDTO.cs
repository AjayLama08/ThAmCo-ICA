using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThAmCo.Events.Dtos
{
    public class ReservationGetDTO
    {
        [JsonPropertyName("reference")]
        public string Reference { get; set; } = string.Empty;
        [JsonPropertyName("eventDate")]
        public DateTime EventDate { get; set; }
        [JsonPropertyName("venueCode")]
        public string VenueCode { get; set; } = string.Empty;
        [JsonPropertyName("whenMade")]
        public DateTime WhenMade { get; set; }
        [JsonPropertyName("staffId")]
        public string StaffId { get; set; } = string.Empty;
    }
}
