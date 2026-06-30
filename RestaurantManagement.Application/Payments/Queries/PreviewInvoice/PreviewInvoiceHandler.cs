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
        //----------------------------------------------------
        // Lấy Order
        //----------------------------------------------------

        var order =
            await _orderRepository
                .GetPaymentOrderAsync(request.OrderId);

        if (order == null)
            throw new Exception("Order not found.");

        //----------------------------------------------------
        // Chỉ được thanh toán khi Chef nấu xong
        //----------------------------------------------------

        if (order.Status != OrderStatus.Ready)
            throw new Exception(
                "Order is not ready.");

        decimal total = order.TotalAmount;

        decimal discount = 0;

        bool hasMember = false;

        string? memberName = null;

        //----------------------------------------------------
        // Có Member Card
        //----------------------------------------------------

        if (order.MemberCard != null)
        {
            hasMember = true;

            memberName =
                order.MemberCard.FullName;

            //------------------------------------------------
            // Ví dụ:
            // 1 điểm = 1.000 VNĐ
            //------------------------------------------------

            discount =
                order.MemberCard.LoyaltyPoints * 1000;

            if (discount > total)
            {
                discount = total;
            }
        }

        return new PreviewInvoiceResponse
        {
            OrderId = order.OrderId,

            Total = total,

            Discount = discount,

            FinalAmount = total - discount,

            HasMemberCard = hasMember,

            MemberName = memberName
        };
    }
}