namespace RestaurantManagement.Domain.Entities;

public class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public Order? Order { get; set; }

    public int DishId { get; set; }

    public Dish? Dish { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal SubTotal { get; set; }
}