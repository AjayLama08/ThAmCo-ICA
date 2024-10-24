using System.ComponentModel.DataAnnotations;


namespace ThAmCo.Catering.Data
{
    public class FoodBooking
    {
        [Required]
        public int FoodBookingId { get; set; }
        [Required]
        public int ClientReferenceId { get; set; }
        [Required]
        public int NumberOfGuests { get; set; }
        [Required]
        public int MenuId { get; set; }

        //Navigation property with Menu class
        public Menu Menu { get; set; }

       

    }
}

