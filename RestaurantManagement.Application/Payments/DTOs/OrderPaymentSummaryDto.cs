namespace RestaurantManagement.Application.Payments.DTOs;

public class OrderPaymentSummaryDto
{
    public int OrderId { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal SubTotal => Quantity * Price;
}