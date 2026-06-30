using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Orders.Queries.GetCart;

public class CartItemDto
{
    public int OrderItemId { get; set; }
    public int DishId { get; set; }
    public string DishName { get; set; }   // ✅ thêm cái này
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
}

public class GetCartResponse
{
    public int OrderId { get; set; }
    public List<CartItemDto> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
}