namespace RestaurantManagement.Application.Payments.Commands.CheckoutPayment;

public class CheckoutPaymentResponse
{
    public int InvoiceId { get; set; }

    public decimal Total { get; set; }

    public decimal Discount { get; set; }

    public decimal FinalAmount { get; set; }

    public string Message { get; set; } = string.Empty;
}