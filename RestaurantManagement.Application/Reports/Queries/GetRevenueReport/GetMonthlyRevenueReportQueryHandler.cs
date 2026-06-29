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
        // orders bây giờ là danh sách các CustomerOrder đã thanh toán
        var orders = await _unitOfWork.Orders.GetPaidOrdersByMonthAsync(request.Month, request.Year);

        var report = new RevenueReportDto
        {
            Month = request.Month,
            Year = request.Year,
            // SỬA TẠI ĐÂY: Lấy tiền từ hóa đơn Invoice.FinalAmount thay vì o.TotalAmount không tồn tại
            TotalRevenue = orders.Sum(o => o.Invoice != null ? o.Invoice.FinalAmount : 0),
            TopSellingDishes = new List<TopSellingDishDto>()
        };

        // Dùng OrderDetails thực tế thay vì OrderItems
        var allOrderDetails = orders.SelectMany(o => o.OrderDetails).ToList();
        
        report.TotalDishesSold = allOrderDetails.Sum(i => i.Quantity);

        // SỬA TẠI ĐÂY: Group theo đúng cấu trúc thực tế của OrderDetail (ItemId, ItemName, HistoricalPrice)
        report.TopSellingDishes = allOrderDetails
            .GroupBy(i => new { i.ItemId, ItemName = i.MenuItem != null ? i.MenuItem.ItemName : "Món ăn" }) 
            .Select(g => new TopSellingDishDto
            {
                DishId = g.Key.ItemId,
                DishName = g.Key.ItemName,
                QuantitySold = g.Sum(i => i.Quantity),
                TotalSales = g.Sum(i => i.Quantity * i.HistoricalPrice) 
            })
            .OrderByDescending(d => d.QuantitySold)
            .ToList();

        return report;
    }
}