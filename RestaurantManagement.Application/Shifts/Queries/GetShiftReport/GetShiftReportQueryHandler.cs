using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Shifts.DTOs;

namespace RestaurantManagement.Application.Shifts.Queries.GetShiftReport;

public class GetShiftReportQueryHandler : IRequestHandler<GetShiftReportQuery, ShiftReportDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetShiftReportQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ShiftReportDto> Handle(GetShiftReportQuery request, CancellationToken cancellationToken)
    {
        // Gọi hàm lấy ca làm việc theo ID từ DB
        var shift = await _unitOfWork.ShiftRepository.GetByIdAsync(request.ShiftId, cancellationToken);

        if (shift == null)
        {
            throw new Exception($"Không tìm thấy ca làm việc với ID: {request.ShiftId}");
        }

        // Map sang DTO để trả về cho Frontend
        return new ShiftReportDto
        {
            ShiftId = shift.ShiftId,
            UserId = shift.UserId,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            StartingCash = shift.StartingCash,
            EndingCash = shift.EndingCash,
            TotalRevenue = shift.TotalRevenue,
            Status = shift.Status.ToString()
        };
    }
}