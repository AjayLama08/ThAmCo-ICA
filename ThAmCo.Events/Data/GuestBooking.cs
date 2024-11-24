using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class GuestBooking
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        public int GuestId { get; set; }

        public bool HasAttended { get; set; }

        //Navigation property with Event class
        public Event Event { get; set; }

        //Navigation property with Guest class
        public Guest? Guest { get; set; }
    }
}
