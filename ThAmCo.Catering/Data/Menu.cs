using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class Menu
    {
        [Required]
        public int MenuId { get; set; }
        [Required]
        public string MenuName { get; set; } = string.Empty;


        //Navigation property with FoodBooking class
        public List<FoodBooking> FoodBookings { get; set; }

        //Navigation property with MenuFoodItem class
        public List<MenuFoodItem> MenuFoodItems { get; set; }

    }
}

