namespace RestaurantManagement.Application.MenuItems.Commands.UpdateMenuItem
{
    public class UpdateMenuItemRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public int? KitchenAreaId { get; set; }
    }
}
