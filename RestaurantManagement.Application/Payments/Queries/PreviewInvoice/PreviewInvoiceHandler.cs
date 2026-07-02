using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Application.Payments.Queries.PreviewInvoice;

public class PreviewInvoiceHandler
    : IRequestHandler<
        PreviewInvoiceQuery,
        PreviewInvoiceResponse>
{
    private readonly IOrderRepository _orderRepository;

    public PreviewInvoiceHandler(
        IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<PreviewInvoiceResponse> Handle(
     PreviewInvoiceQuery request,
     CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetPaymentOrderAsync(request.OrderId);

        if (order == null)
            throw new Exception("Order not found.");

        if (order.Status != OrderStatus.Ready)
            throw new Exception("Order is not ready.");

        var member = order.MemberCard; // 👈 phải có FK mới ra

        bool hasMember = member != null;

        string? memberName = member?.FullName;
        int loyaltyPoints = member?.LoyaltyPoints ?? 0;

        decimal total = order.TotalAmount;
        decimal discount = hasMember ? loyaltyPoints * 1000 : 0;

        if (discount > total)
            discount = total;

        return new PreviewInvoiceResponse
        {
            OrderId = order.OrderId,
            Total = total,
            Discount = discount,
            FinalAmount = total - discount,
            HasMemberCard = hasMember,
            MemberName = memberName,
            LoyaltyPoints = loyaltyPoints
        };
    }
}