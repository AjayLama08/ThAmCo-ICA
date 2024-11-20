using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Guest
    {

        public Guest()
        {
        }

        public Guest(int guestId, string firstName, string lastName, string email)
        {
            GuestId = guestId;
            FirstName = firstName;
            Lastname = lastName;
            Email = email;
        }

        [Required]
        public int GuestId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string Lastname { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        //Navigation property to GuestBooking class
        public List<GuestBooking> GuestBookings { get; set; }
    }
}
