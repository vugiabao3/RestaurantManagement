using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RestaurantManagement.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest<CancelOrderResponse>
    {
        public int OrderId { get; set; }
        [Required, StringLength(500, MinimumLength = 3)]
        public string Reason { get; set; } = string.Empty;
    }
}
