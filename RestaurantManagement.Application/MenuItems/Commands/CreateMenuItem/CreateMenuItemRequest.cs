namespace RestaurantManagement.Application.MenuItems.Commands.CreateMenuItem
{
    public class CreateMenuItemRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int? KitchenAreaId { get; set; }
    }
}
