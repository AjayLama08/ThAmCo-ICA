using ThAmCo.Catering.Data;
namespace ThAmCo.Catering.DTO
{
    public class FoodItemDTO
    {
        public int FoodItemId { get; set; }
        public string Description { get; set; } = string.Empty;

        public  float UnitPrice { get; set; }


    }
}
