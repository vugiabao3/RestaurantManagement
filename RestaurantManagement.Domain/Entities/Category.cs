namespace RestaurantManagement.Domain.Entities;

public class Category
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Navigation Property (1-N với MenuItem)
    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}