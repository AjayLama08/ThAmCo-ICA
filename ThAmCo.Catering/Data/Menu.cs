﻿using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class Menu
    {
        [Required]
        public int MenuId { get; set; }
        [Required]
        public string MenuName { get; set; }

        public List<FoodBooking> FoodBookings { get; set; }

        public List<MenuFoodItem> MenuFoodItems { get; set; }

    }
}

