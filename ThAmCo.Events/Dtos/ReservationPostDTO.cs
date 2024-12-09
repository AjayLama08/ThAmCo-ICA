using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ThAmCo.Events.Dtos
{
    public class ReservationPostDTO
    {
        public DateTime EventDate { get; set; }
        public string VenueCode { get; set; } = string.Empty;
        public string StaffId { get; set; } = string.Empty;
    }
}
