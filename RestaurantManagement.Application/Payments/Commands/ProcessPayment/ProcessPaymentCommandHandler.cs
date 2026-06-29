using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Application.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Payments.Commands.ProcessPayment;

public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public ProcessPaymentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        // 1. Lấy thông tin Order kèm Chi tiết món và Bàn
        var order = await _unitOfWork.OrderRepository.GetQueryable()
            .Include(o => o.DiningTable)
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

        if (order == null || order.OrderStatus != OrderStatus.Unpaid)
        {
            throw new Exception("Hóa đơn không tồn tại hoặc đã được thanh toán.");
        }

        // 2. Tính tổng tiền thực tế (SubTotal)
        decimal subTotalAmount = order.OrderDetails.Sum(od => od.HistoricalPrice * od.Quantity);
        if (subTotalAmount <= 0) throw new Exception("Tổng hóa đơn bằng 0. Không thể thanh toán.");

        // ==========================================
        // BỔ SUNG LOGIC THẺ THÀNH VIÊN (UC_PAY_03)
        // ==========================================
        decimal discountAmount = 0;
        decimal finalAmount = subTotalAmount;
        MemberCard? member = null;

        if (request.MemberId.HasValue)
        {
            // Cần thêm MemberCardRepository vào IUnitOfWork sau nhé
            member = await _unitOfWork.MemberRepository.GetQueryable()
                .FirstOrDefaultAsync(m => m.MemberId == request.MemberId.Value, cancellationToken);

            if (member != null)
            {
                // Giả định: Request có thêm thuộc tính PointsToUse (Số điểm khách muốn dùng)
                // Vì gấp, mình code cứng: Khách có bao nhiêu điểm, quy đổi hết ra tiền để giảm giá (Theo BR02: 1 điểm = 1000 VNĐ)
                if (member.LoyaltyPoints >= 10) // BR02: Chỉ được dùng khi >= 10 điểm
                {
                    discountAmount = member.LoyaltyPoints * 1000;
                    
                    // Nếu tiền giảm giá > tổng tiền hóa đơn thì chỉ giảm tối đa bằng tổng tiền
                    if (discountAmount > subTotalAmount) discountAmount = subTotalAmount;
                    
                    finalAmount = subTotalAmount - discountAmount;
                    member.LoyaltyPoints = 0; // Trừ hết điểm cũ
                }

                // BR01: Cộng điểm mới dựa trên FinalAmount (100k = 1 điểm)
                int newPointsEarned = (int)(finalAmount / 100000);
                member.LoyaltyPoints += newPointsEarned;
                

            }
        }
        // ==========================================

        // 3. Kiểm tra Alternative Flow 2a: Khách đưa thiếu tiền
        if (request.CashReceived < finalAmount)
        {
            throw new Exception("MS03: Số tiền khách đưa không đủ để thanh toán hóa đơn.");
        }

        // 4. Cập nhật trạng thái
        order.OrderStatus = OrderStatus.Paid;
        if (order.DiningTable != null)
        {
            order.DiningTable.CurrentStatus = TableStatus.Available;
        }

        // 5. Tạo Hóa đơn (Invoice)
        var invoice = new Invoice
        {
            OrderId = order.OrderId,
            CashierId = request.CashierId,
            MemberId = request.MemberId,
            PaymentTime = DateTime.Now,
            PaymentMethod = PaymentMethod.Cash,
            SubTotalAmount = subTotalAmount,
            DiscountAmount = discountAmount,
            DiscountReason = discountAmount > 0 ? "Trừ điểm thành viên" : null,
            FinalAmount = finalAmount
        };

        await _unitOfWork.InvoiceRepository.AddAsync(invoice, cancellationToken);

        // 6. THỰC THI TRANSACTION CHO CẢ 4 BẢNG (Order, Table, MemberCard, Invoice)
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return invoice.InvoiceId;
    }
}