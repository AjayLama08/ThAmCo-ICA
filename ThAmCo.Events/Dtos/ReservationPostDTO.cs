using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Dtos
{
    public class ReservationPostDTO
    {
        public DateTime EventDate { get; set; }
        public string VenueCode { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string StaffId { get; set; } = string.Empty;
    }
}
