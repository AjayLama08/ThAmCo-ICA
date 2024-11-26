using System.ComponentModel.DataAnnotations;
using System.Diagnostics;


namespace ThAmCo.Events.Data
{
    public class Event
    {
        public Event()
        {
        }

        public Event(int eventId, string eventTypeId, string title, DateTime date)
        {
            EventId = eventId;
            EventTypeId = eventTypeId;
            Title = title;
            DateAndTime = date;
        }
       
        public int EventId { get; set; }
        [Required]
        public string EventTypeId { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateAndTime { get; set; }
  
        public int? FoodBookingId { get; set; }

        public string? ReservationReference { get; set; }

        //Navigation property to Staffing class
        public List<Staffing> Staffings { get; set; }

        //Navigation property to GuestBooking class
        public List<GuestBooking> GuestBookings { get; set; }




    }
}
