using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.ViewModels
{
    public class ReservationPostVM
    {
        public int EventId { get; set; }

        [Display(Name = "Event Type ID")]
        public string EventTypeId { get; set; } = string.Empty;

        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Date and Time")]
        public DateTime DateAndTime { get; set; }

        [Display(Name = "Reservation Reference")]
        public string? ReservationReference { get; set; }

        public int? FoodBookingId { get; set; }
    }
}
