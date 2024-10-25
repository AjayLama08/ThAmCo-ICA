using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class FoodItem
    {
        [Required]
        public int FoodItemId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public float UnitPrice { get; set; }


        //Navigation property with MenuFoodItem class
        public List<MenuFoodItem> MenuFoodItems { get; set; }
    }
}
