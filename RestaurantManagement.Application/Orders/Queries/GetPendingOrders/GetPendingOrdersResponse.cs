namespace RestaurantManagement.Application.Orders.Queries.GetPendingOrders;

public class GetPendingOrdersResponse
{
    public int OrderId { get; set; }


    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public string DishName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal SubTotal { get; set; }
}