namespace RestaurantManagement.Application.MenuItems.Commands.DeleteMenuItem
{
    public class DeleteMenuItemResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool RealtimeSynchronized { get; set; }
    }
}
