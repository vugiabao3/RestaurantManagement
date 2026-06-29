using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Shifts.Commands.CloseShift;

public class CloseShiftCommandHandler : IRequestHandler<CloseShiftCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public CloseShiftCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CloseShiftCommand request, CancellationToken cancellationToken)
    {
        // 1. Tìm ca làm việc đang mở của nhân viên này
        var activeShift = await _unitOfWork.ShiftRepository.GetActiveShiftAsync(request.UserId, cancellationToken);
        
        if (activeShift == null)
        {
            throw new Exception("Không tìm thấy ca làm việc nào đang mở cho nhân viên này.");
        }

        // 2. Cập nhật thông tin chốt ca
        activeShift.EndTime = DateTime.Now;
        activeShift.EndingCash = request.EndingCash;
        
        // Tạm tính doanh thu trong ca = Tiền cuối ca đếm được - Tiền lẻ đầu ca
        // (Nếu bạn có bảng Invoice liên kết với ShiftId, có thể Query Sum(TotalAmount) ở đây để tính chính xác hơn)
        activeShift.TotalRevenue = request.EndingCash - activeShift.StartingCash; 
        
        activeShift.Status = ShiftStatus.Closed;

        // 3. Lưu vào DB
        _unitOfWork.ShiftRepository.Update(activeShift);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}