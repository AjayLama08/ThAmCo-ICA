using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class MenuFoodItem
    {
        public int MenuId { get; set; }
        public int FoodItemId { get; set; }

        //Navigation property with Menu class
        public Menu Menu { get; set; }

        //Navigation property with FoodItem class
        public FoodItem FoodItem { get; set; }
    }
}
