using System.Collections.Generic;
using MediatR;

namespace RestaurantManagement.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<int>
{
    public int TableId { get; set; }
    public List<OrderItemRequest> Items { get; set; } = new();
}

public class OrderItemRequest
{
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public decimal HistoricalPrice { get; set; } 
}