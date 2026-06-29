using MediatR;
using RestaurantManagement.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Reports.Queries.GetRevenueReport;

public class GetMonthlyRevenueReportQueryHandler : IRequestHandler<GetMonthlyRevenueReportQuery, RevenueReportDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMonthlyRevenueReportQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RevenueReportDto> Handle(GetMonthlyRevenueReportQuery request, CancellationToken cancellationToken)
    {
        var orders = await _unitOfWork.Orders.GetPaidOrdersByMonthAsync(request.Month, request.Year);

        var report = new RevenueReportDto
        {
            Month = request.Month,
            Year = request.Year,
            // Đã sửa: Tính tổng doanh thu bằng cách nhân Quantity với HistoricalPrice của từng OrderDetail
            TotalRevenue = orders.Sum(o => o.OrderDetails.Sum(od => od.Quantity * od.HistoricalPrice)),
            TopSellingDishes = new List<TopSellingDishDto>()
        };

        var allOrderDetails = orders.SelectMany(o => o.OrderDetails).ToList();
        
        report.TotalDishesSold = allOrderDetails.Sum(i => i.Quantity);

        report.TopSellingDishes = allOrderDetails
            // Đã sửa: Dùng ItemId và MenuItem.ItemName của bảng OrderDetail
            .GroupBy(i => new { i.ItemId, i.MenuItem?.ItemName }) 
            .Select(g => new TopSellingDishDto
            {
                DishId = g.Key.ItemId, // Gán ItemId vào DTO
                DishName = g.Key.ItemName ?? "Unknown Item", // Lấy tên từ bảng MenuItem
                QuantitySold = g.Sum(i => i.Quantity),
                TotalSales = g.Sum(i => i.Quantity * i.HistoricalPrice) // Dùng HistoricalPrice để tính doanh số món
            })
            .OrderByDescending(d => d.QuantitySold)
            .Take(10)
            .ToList();

        return report;
    }
}