using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Application.Payments.Commands.CheckoutPayment;

public class CheckoutPaymentHandler
    : IRequestHandler<CheckoutPaymentCommand, CheckoutPaymentResponse>
{
    private readonly IOrderRepository _orderRepo;
    private readonly IInvoiceRepository _invoiceRepo;
    private readonly ITableRepository _tableRepo;
    private readonly IMemberRepository _memberRepo;

    public CheckoutPaymentHandler(
        IOrderRepository orderRepo,
        IInvoiceRepository invoiceRepo,
        ITableRepository tableRepo,
        IMemberRepository memberRepo)
    {
        _orderRepo = orderRepo;
        _invoiceRepo = invoiceRepo;
        _tableRepo = tableRepo;
        _memberRepo = memberRepo;
    }

    public async Task<CheckoutPaymentResponse> Handle(
        CheckoutPaymentCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepo.GetByIdAsync(request.OrderId);

        if (order == null)
            throw new Exception("Order not found");

        if (order.Status != "Ready")
            throw new Exception("Order not ready");

        decimal total = order.TotalAmount;
        decimal discount = 0;

        //------------------------------------------------
        // MEMBER DISCOUNT
        //------------------------------------------------
        MemberCard? member = null;

        if (request.MemberId.HasValue)
        {
            member = await _memberRepo.GetByIdAsync(request.MemberId.Value);

            if (member != null)
            {
                discount = member.LoyaltyPoints * 1000;

                if (discount > total)
                    discount = total;

                // update điểm (logic đơn giản)
                member.LoyaltyPoints += (int)(total / 10000);
            }
        }

        decimal finalAmount = total - discount;

        //------------------------------------------------
        // CREATE INVOICE
        //------------------------------------------------
        var invoice = new Invoice
        {
            OrderId = order.OrderId,
            MemberId = request.MemberId,
            TotalAmount = total,
            DiscountAmount = discount,
            FinalAmount = finalAmount,
            PaymentMethod = Enum.Parse<PaymentMethod>(request.PaymentMethod),
            PaidAt = DateTime.Now
        };

        await _invoiceRepo.AddAsync(invoice);

        //------------------------------------------------
        // UPDATE ORDER
        //------------------------------------------------
        order.Status = "Paid";
        await _orderRepo.UpdateAsync(order);

        //------------------------------------------------
        // UPDATE TABLE
        //------------------------------------------------
        var table = await _tableRepo.GetByIdAsync(order.TableId);

        if (table != null)
        {
            table.CurrentStatus = TableStatus.Available;
            await _tableRepo.UpdateAsync(table);
        }

        //------------------------------------------------
        // SHIFT UPDATE (simplified)
        //------------------------------------------------
        
        //------------------------------------------------
        // RESULT
        //------------------------------------------------
        return new CheckoutPaymentResponse
        {
            InvoiceId = invoice.InvoiceId,
            Total = total,
            Discount = discount,
            FinalAmount = finalAmount,
            Message = "Payment successful"
        };
    }
}