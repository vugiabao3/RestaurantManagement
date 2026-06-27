using MediatR;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Application.Payments.Commands.ProcessPayment;

// Dữ liệu truyền vào từ UI khi thu ngân bấm nút "Thanh toán"
public record ProcessPaymentCommand(
    int OrderId, 
    int CashierId, 
    PaymentMethod PaymentMethod,
    int? MemberId = null,
    string? DiscountReason = null
) : IRequest<int>; // Trả về InvoiceId vừa tạo