using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RestaurantManagement.Domain.Constants;

namespace RestaurantManagement.API.Hubs
{
    [Authorize]
    public class KitchenHub : Hub
    {
        private bool IsKitchenUser() =>
            Context.User?.IsInRole(UserRoles.Admin) == true || Context.User?.IsInRole(UserRoles.Kitchen) == true;

        private bool IsStaffUser() =>
            Context.User?.IsInRole(UserRoles.Admin) == true || Context.User?.IsInRole(UserRoles.Staff) == true;

        public override async Task OnConnectedAsync()
        {
            if (IsKitchenUser())
                await Groups.AddToGroupAsync(Context.ConnectionId, "kitchen");
            if (IsStaffUser())
                await Groups.AddToGroupAsync(Context.ConnectionId, "staff");
            await base.OnConnectedAsync();
        }

        public Task JoinKitchenArea(int kitchenAreaId)
        {
            if (!IsKitchenUser()) throw new HubException("Bạn không có quyền theo dõi khu vực bếp");
            return Groups.AddToGroupAsync(Context.ConnectionId, $"kitchen-area-{kitchenAreaId}");
        }

        public Task LeaveKitchenArea(int kitchenAreaId) =>
            Groups.RemoveFromGroupAsync(Context.ConnectionId, $"kitchen-area-{kitchenAreaId}");

        public Task JoinKitchen()
        {
            if (!IsKitchenUser()) throw new HubException("Bạn không có quyền theo dõi màn hình bếp");
            return Groups.AddToGroupAsync(Context.ConnectionId, "kitchen");
        }

        public Task JoinStaff()
        {
            if (!IsStaffUser()) throw new HubException("Bạn không có quyền nhận thông báo phục vụ");
            return Groups.AddToGroupAsync(Context.ConnectionId, "staff");
        }
    }
}
