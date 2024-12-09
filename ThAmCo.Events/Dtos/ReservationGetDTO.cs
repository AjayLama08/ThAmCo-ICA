using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThAmCo.Events.Dtos
{
    public class ReservationGetDTO
    {
        public string Reference { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string VenueCode { get; set; } = string.Empty;
        public DateTime WhenMade { get; set; }
        public string StaffId { get; set; } = string.Empty;
    }
}
