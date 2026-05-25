using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RestaurantManagement.Application.Interfaces
{
    public interface IReportRepository
    {
        Task<List<RevenueReport>>
            GetRevenueReportsAsync(
                DateTime startDate,
                DateTime endDate);
    }
}
