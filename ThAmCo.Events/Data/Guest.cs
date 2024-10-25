using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Guest
    {
        [Required]
        public int GuestId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        //Navigation property to GuestBooking class
        public List<GuestBooking> GuestBookings { get; set; }
    }
}
