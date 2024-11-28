using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Dtos
{
    public class ReservationGetDTO
    {
        public string Reference { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public string VenueCode { get; set; } = string.Empty;

        public DateTime WhenMade { get; set; }

        public string StaffName { get; set; } = string.Empty;
    }
}
