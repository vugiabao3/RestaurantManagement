using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RestaurantManagement.Application.Orders.Commands.UpdateOrderPriority
{
    public class UpdateOrderPriorityCommand : IRequest<UpdateOrderPriorityResponse>
    {
        public int OrderId { get; set; }

        [Range(0, 1)]
        public int Priority { get; set; }
    }

    public class UpdateOrderPriorityResponse
    {
        public string Message { get; set; } = string.Empty;
        public int OrderId { get; set; }
        public int Priority { get; set; }
        public int Version { get; set; }
        public bool RealtimeSynchronized { get; set; }
    }
}
