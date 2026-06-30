using MediatR;

namespace RestaurantManagement.Application.Payments.Commands.CheckoutPayment;

public class CheckoutPaymentCommand : IRequest<CheckoutPaymentResponse>
{
    public int OrderId { get; set; }

    public int? MemberId { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;
}