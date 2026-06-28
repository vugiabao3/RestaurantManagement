namespace RestaurantManagement.Application.MenuItems.Queries.GetMenuCategories
{
    public class MenuCategorySummaryDto
    {
        public string Category { get; set; } = string.Empty;

        public int ItemCount { get; set; }
    }
}
