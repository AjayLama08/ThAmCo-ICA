using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.ViewModels
{
    public class ReserveVenueVM
    {
        public int EventId { get; set; }
        public string EventTypeId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime DateAndTime { get; set; }
        public int? FoodBookingId { get; set; }
        public string? ReservationReference { get; set; }
    }
}
