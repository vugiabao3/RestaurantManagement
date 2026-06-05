using Microsoft.AspNetCore.SignalR;

namespace RestaurantManagement.Api.Hubs;

public class TrackingHub : Hub
{
    public Task JoinTableGroup(int tableId)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, $"table-{tableId}");
    }

    public Task LeaveTableGroup(int tableId)
    {
        return Groups.RemoveFromGroupAsync(Context.ConnectionId, $"table-{tableId}");
    }
}