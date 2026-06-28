using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RestaurantManagement.Application.Orders.Commands.UndoOrderDetailStatus
{
    public class UndoOrderDetailStatusCommand : IRequest<UndoOrderDetailStatusResponse>
    {
        public int OrderDetailId { get; set; }
        [Range(1, int.MaxValue)] public int Version { get; set; }
    }
}
