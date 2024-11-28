using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.ViewModels
{
    public class ReservationGetVM
    {
        public int EventId { get; set; }
        
        public string EventTypeId { get; set; } = string.Empty;

        [Display(Name = "Event Title")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Date and Time")]
        public DateTime DateAndTime { get; set; }

        public int? FoodBookingId { get; set; }

        [Display(Name = "Reservation Reference")]
        public string? ReservationReference { get; set; }


    }
}
