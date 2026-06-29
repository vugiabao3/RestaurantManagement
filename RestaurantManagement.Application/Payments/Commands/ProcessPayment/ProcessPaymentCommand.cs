using MediatR;

namespace RestaurantManagement.Application.Payments.Commands.ProcessPayment;

// Trả về int chính là InvoiceId để Controller mang đi in hóa đơn
public class ProcessPaymentCommand : IRequest<int>
{
    public int OrderId { get; set; }
    public decimal CashReceived { get; set; } // Số tiền khách đưa
    public int CashierId { get; set; } // ID của thu ngân đang thao tác
    public int? MemberId { get; set; } // Dành cho UC_PAY_03 (Thẻ thành viên) sau này
}