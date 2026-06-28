namespace RestaurantManagement.Application.MenuItems.Commands.CreateMenuItem
{
    public class CreateMenuItemResponse
    {
        public string Message { get; set; } = string.Empty;
        public int MenuItemId { get; set; }
        public bool RealtimeSynchronized { get; set; }
    }
}
