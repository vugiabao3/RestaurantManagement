using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Reports.Queries.GetRevenueReport
{
    public class GetRevenueReportHandler
        : IRequestHandler<
            GetRevenueReportQuery,
            List<GetRevenueReportResponse>>
    {
        private readonly IReportRepository _repository;

        public GetRevenueReportHandler(
            IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetRevenueReportResponse>>
            Handle(
                GetRevenueReportQuery request,
                CancellationToken cancellationToken)
        {
            var reports = await _repository
                .GetRevenueReportsAsync(
                    request.StartDate,
                    request.EndDate);

            return reports.Select(x =>
                new GetRevenueReportResponse
                {
                    ReportId = x.ReportId,
                    BranchId = x.BranchId,
                    TotalOrders = x.TotalOrders,
                    TotalRevenue = x.TotalRevenue,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).ToList();
        }
    }
}
