using MediatR;
using RestaurantManagement.Application.Reports.Queries.GetRevenueReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Reports.Queries.GetRevenueReport
{
    public class GetRevenueReportQuery
        : IRequest<List<GetRevenueReportResponse>>
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}