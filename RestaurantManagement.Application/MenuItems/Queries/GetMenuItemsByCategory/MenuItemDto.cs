namespace RestaurantManagement.Application.MenuItems.Queries.GetMenuItemsByCategory
{
    public class MenuItemDto
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public int? KitchenAreaId { get; set; }
        public string? KitchenAreaName { get; set; }
    }
}
