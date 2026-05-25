using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RestaurantManagement.Application.Reports.Queries.GetRevenueReport
{
    public class GetRevenueReportResponse
    {
        public Guid ReportId { get; set; }

        public Guid BranchId { get; set; }

        public int TotalOrders { get; set; }

        public decimal TotalRevenue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}