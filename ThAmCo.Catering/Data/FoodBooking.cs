﻿using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class FoodBooking
    {
        public int FoodBookingId { get; set; }
        public int ClientReferenceId { get; set; }
        public int NumberOfGuests { get; set; }
        public int MenuId { get; set; }

        //Navigation property with Menu class
        public Menu Menu { get; set; }

        


    }
}

