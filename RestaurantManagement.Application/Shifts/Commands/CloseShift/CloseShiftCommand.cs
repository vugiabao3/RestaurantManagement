using MediatR;

namespace RestaurantManagement.Application.Shifts.Commands.CloseShift;

public class CloseShiftCommand : IRequest<bool>
{
    public int UserId { get; set; }
    public decimal EndingCash { get; set; } // Tiền mặt thực tế đếm được trong két khi chốt ca
}