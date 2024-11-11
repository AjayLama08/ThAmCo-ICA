using System.ComponentModel.DataAnnotations;
using System.Diagnostics;


namespace ThAmCo.Events.Data
{
    public class Event
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        public string EventTypeId { get; set; } = string.Empty;

        [Required]
        public string Title { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int? FoodBookingId { get; set; }

        public bool HasReservation { get; set; }

        //Navigation property to Staffing class
        public List<Staffing> Staffings { get; set; }

        //Navigation property to GuestBooking class
        public List<GuestBooking> GuestBookings { get; set; }




    }
}
