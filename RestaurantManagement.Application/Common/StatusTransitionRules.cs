using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Common
{
    public static class StatusTransitionRules
    {
        private static readonly Dictionary<string, string[]> DetailTransitions = new(StringComparer.OrdinalIgnoreCase)
        {
            [OrderStatus.Pending] = new[] { OrderStatus.Preparing, OrderStatus.OutOfStock },
            [OrderStatus.Preparing] = new[] { OrderStatus.Ready, OrderStatus.OutOfStock }
        };

        private static readonly Dictionary<string, string[]> OrderTransitions = new(StringComparer.OrdinalIgnoreCase)
        {
            [OrderStatus.Pending] = new[] { OrderStatus.Preparing, OrderStatus.Cancelled },
            [OrderStatus.Preparing] = new[] { OrderStatus.Ready, OrderStatus.Cancelled }
        };

        public static void EnsureOrderDetailTransition(string oldStatus, string newStatus)
        {
            if (!DetailTransitions.TryGetValue(oldStatus, out var allowed) ||
                !allowed.Contains(newStatus, StringComparer.OrdinalIgnoreCase))
            {
                throw new BusinessException($"Không được chuyển trạng thái món từ {oldStatus} sang {newStatus}");
            }
        }

        public static void EnsureOrderTransition(string oldStatus, string newStatus)
        {
            if (!OrderTransitions.TryGetValue(oldStatus, out var allowed) ||
                !allowed.Contains(newStatus, StringComparer.OrdinalIgnoreCase))
            {
                throw new BusinessException($"Không được chuyển trạng thái Order từ {oldStatus} sang {newStatus}");
            }
        }
    }
}
