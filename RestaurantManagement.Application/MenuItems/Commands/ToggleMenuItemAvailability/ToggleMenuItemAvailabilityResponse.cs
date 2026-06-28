namespace RestaurantManagement.Application.MenuItems.Commands.ToggleMenuItemAvailability
{
    public class ToggleMenuItemAvailabilityResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public bool RealtimeSynchronized { get; set; }
    }
}
