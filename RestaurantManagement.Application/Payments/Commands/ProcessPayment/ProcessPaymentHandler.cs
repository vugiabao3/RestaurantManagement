using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Interfaces.Repositories;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Application.Payments.Commands.ProcessPayment;

public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProcessPaymentHandler(
        IOrderRepository orderRepository, 
        IInvoiceRepository invoiceRepository, 
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        // 1. Kiểm tra đơn hàng hợp lệ
        var order = await _orderRepository.GetOrderForPaymentAsync(request.OrderId, cancellationToken);
        if (order == null || order.OrderStatus == OrderStatus.Paid)
        {
            throw new Exception("Đơn hàng không hợp lệ hoặc đã được thanh toán.");
        }

        // 2. Tính toán tiền
        decimal subTotal = order.OrderDetails.Sum(od => od.Quantity * od.HistoricalPrice);
        decimal discountAmount = 0; // Giả sử tính logic khuyến mãi ở đây (VD: VIP giảm 10%)
        
        // (Nếu có nghiệp vụ MemberCard, bạn gọi IMemberRepository ở đây để kiểm tra hạng thẻ)

        decimal finalAmount = subTotal - discountAmount;

        // 3. Tạo Hóa đơn (Invoice Entity)
        var invoice = new Invoice
        {
            OrderId = request.OrderId,
            CashierId = request.CashierId,
            MemberId = request.MemberId,
            PaymentTime = DateTime.Now,
            PaymentMethod = request.PaymentMethod,
            SubTotalAmount = subTotal,
            DiscountAmount = discountAmount,
            DiscountReason = request.DiscountReason,
            FinalAmount = finalAmount
        };

        // 4. Cập nhật trạng thái hệ thống
        order.OrderStatus = OrderStatus.Paid;
        
        if (order.DiningTable != null)
        {
            // Giải phóng bàn trống sau khi khách thanh toán xong
            order.DiningTable.CurrentStatus = TableStatus.Available; 
        }

        // 5. Lưu vào CSDL (Transaction)
        await _invoiceRepository.AddAsync(invoice, cancellationToken);
        _orderRepository.Update(order);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 6. Trả về mã Hóa đơn để In
        return invoice.InvoiceId;
    }
}