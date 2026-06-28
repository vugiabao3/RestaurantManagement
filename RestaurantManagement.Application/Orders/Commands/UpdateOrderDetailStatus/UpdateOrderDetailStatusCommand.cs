using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RestaurantManagement.Application.Orders.Commands.UpdateOrderDetailStatus
{
    public class UpdateOrderDetailStatusCommand : IRequest<UpdateOrderDetailStatusResponse>
    {
        public int OrderDetailId { get; set; }
        [Required, StringLength(50)] public string Status { get; set; } = string.Empty;
        [Range(1, int.MaxValue)] public int Version { get; set; }
    }
}
