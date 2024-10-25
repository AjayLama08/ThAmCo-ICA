using System.ComponentModel.DataAnnotations;
using System.Diagnostics;


namespace ThAmCo.Events.Data
{
    public class Event
    {
        public int EventId { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        //Navigation property to Staffing class
        public List<Staffing> Staffings { get; set; }

        //Navigation property to GuestBooking class
        public List<GuestBooking> GuestBookings { get; set; }




    }
}
