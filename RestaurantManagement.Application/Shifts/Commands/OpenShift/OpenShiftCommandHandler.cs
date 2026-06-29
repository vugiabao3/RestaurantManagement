using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities; // Chứa class Shift

namespace RestaurantManagement.Application.Shifts.Commands.OpenShift;

public class OpenShiftCommandHandler : IRequestHandler<OpenShiftCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public OpenShiftCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(OpenShiftCommand request, CancellationToken cancellationToken)
    {
        // Kiểm tra xem user này có đang trong 1 ca làm việc chưa đóng hay không
        var activeShift = await _unitOfWork.ShiftRepository.GetActiveShiftAsync(request.UserId, cancellationToken);
        if (activeShift != null)
        {
            throw new Exception("Bạn đang có một ca làm việc chưa đóng. Vui lòng chốt ca cũ trước khi mở ca mới.");
        }

        var newShift = new Shift
        {
            // Khởi tạo các thuộc tính theo Entity Shift của bạn
            // UserId = request.UserId,
            // StartTime = DateTime.UtcNow,
            // StartingCash = request.StartingCash,
            // Status = ShiftStatus.Active
        };

        await _unitOfWork.ShiftRepository.AddAsync(newShift, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newShift.ShiftId; // Giả sử Entity Shift có khóa chính là ShiftId
    }
}