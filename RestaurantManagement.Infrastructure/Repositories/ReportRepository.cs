using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories
{
    public class ReportRepository
        : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RevenueReport>>
            GetRevenueReportsAsync(
                DateTime startDate,
                DateTime endDate)
        {
            return await _context.RevenueReports
                .Where(x =>
                    x.StartDate >= startDate
                    &&
                    x.EndDate <= endDate)
                .ToListAsync();
        }
    }
}