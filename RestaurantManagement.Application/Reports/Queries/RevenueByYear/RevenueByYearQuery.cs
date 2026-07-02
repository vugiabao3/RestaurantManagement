using MediatR;

namespace RestaurantManagement.Application.Reports.Queries.RevenueByYear;

public class RevenueByYearQuery
    : IRequest<RevenueByYearResponse>
{
    public int Year { get; set; }
}