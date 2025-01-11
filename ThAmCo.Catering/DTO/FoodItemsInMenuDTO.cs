namespace ThAmCo.Catering.DTO
{
    public class FoodItemsInMenuDTO
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public List<FoodItemGetDTO> FoodItems { get; set; }

    }
}
