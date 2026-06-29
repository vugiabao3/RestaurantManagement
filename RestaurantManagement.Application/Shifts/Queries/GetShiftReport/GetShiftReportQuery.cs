using MediatR;
using RestaurantManagement.Application.Shifts.DTOs;

namespace RestaurantManagement.Application.Shifts.Queries.GetShiftReport;

public class GetShiftReportQuery : IRequest<ShiftReportDto>
{
    public int ShiftId { get; set; }

    public GetShiftReportQuery(int shiftId)
    {
        ShiftId = shiftId;
    }
}