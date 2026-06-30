using MediatR;

namespace RestaurantManagement.Application.Orders.Commands.AddDishToCart;

public class AddDishToCartCommand
    : IRequest<AddDishToCartResponse>
{
    public int CustomerId { get; set; }

    public int DishId { get; set; }

    public int Quantity { get; set; }

    // Bàn khách đang ngồi
    public int TableId { get; set; }

    // Có thể không có thẻ thành viên
    public int? MemberId { get; set; }
}