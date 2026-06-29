using MediatR;

namespace RestaurantManagement.Application.Reports.Queries.GetRevenueReport;

// Class này chỉ chứa tham số đầu vào, KHÔNG chứa logic xử lý
public class GetMonthlyRevenueReportQuery : IRequest<RevenueReportDto>
{
    public int Month { get; set; }
    public int Year { get; set; }

    public GetMonthlyRevenueReportQuery(int month, int year)
    {
        Month = month;
        Year = year;
    }
}