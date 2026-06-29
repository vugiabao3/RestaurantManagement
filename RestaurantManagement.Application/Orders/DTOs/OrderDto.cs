using System;
using System.Collections.Generic;

namespace RestaurantManagement.Application.Orders.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    public int TableId { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public string OrderStatus { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; } = new();
}

public class OrderDetailDto
{
    public int MenuItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal => Quantity * UnitPrice;
}