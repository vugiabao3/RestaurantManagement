using MediatR;

namespace RestaurantManagement.Application.Shifts.Commands.OpenShift;

public class OpenShiftCommand : IRequest<int>
{
    public int UserId { get; set; } // ID của thu ngân
    public decimal StartingCash { get; set; } // Tiền lẻ ban đầu trong két
}