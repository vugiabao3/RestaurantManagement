using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Reports.Queries.RevenueByYear;

public class RevenueByYearHandler
    : IRequestHandler<
        RevenueByYearQuery,
        RevenueByYearResponse>
{
    private readonly IInvoiceRepository _invoiceRepository;

    public RevenueByYearHandler(
        IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<RevenueByYearResponse> Handle(
        RevenueByYearQuery request,
        CancellationToken cancellationToken)
    {
        var invoices =
            await _invoiceRepository
                .GetInvoicesByYearAsync(request.Year);

        var result = new RevenueByYearResponse
        {
            Year = request.Year
        };

        // Luôn trả về đủ 12 tháng
        for (int month = 1; month <= 12; month++)
        {
            decimal revenue = invoices
                .Where(x => x.PaidAt.Month == month)
                .Sum(x => x.FinalAmount);

            result.Months.Add(new MonthlyRevenueDto
            {
                Month = month,
                Revenue = revenue
            });
        }

        result.TotalRevenue =
            result.Months.Sum(x => x.Revenue);

        return result;
    }
}