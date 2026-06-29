namespace RestaurantManagement.Application.Payments.Queries.GetOrderForPayment;

public class OrderSummaryDto
{
    public int OrderId { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
}

public class OrderItemDto
{
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal => Quantity * UnitPrice; // Tính thành tiền từng món
}