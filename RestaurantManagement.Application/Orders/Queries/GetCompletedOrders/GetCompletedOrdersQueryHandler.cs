using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Orders.DTOs;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Orders.Queries.GetCompletedOrders;

public class GetCompletedOrdersQueryHandler : IRequestHandler<GetCompletedOrdersQuery, List<OrderDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCompletedOrdersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<OrderDto>> Handle(GetCompletedOrdersQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.OrderRepository.GetQueryable()
            .Include(o => o.DiningTable)
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.MenuItem)
            .Where(o => o.OrderStatus == OrderStatus.Paid);

        // Nếu có truyền ngày vào thì lọc theo ngày đó (ví dụ: chỉ lấy hóa đơn hôm nay)
        if (request.FilterDate.HasValue)
        {
            var date = request.FilterDate.Value.Date;
            query = query.Where(o => o.StartTime.Date == date);
        }

        var completedOrders = await query
            .OrderByDescending(o => o.StartTime)
            .ToListAsync(cancellationToken);

        return completedOrders.Select(o => new OrderDto
        {
            OrderId = o.OrderId,
            TableId = o.TableId,
            TableNumber = o.DiningTable?.TableNumber ?? string.Empty,
            OrderStatus = o.OrderStatus.ToString(),
            CreatedAt = o.StartTime,
            OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
            {
                MenuItemId = od.ItemId,
                ItemName = od.MenuItem?.ItemName ?? "Món không xác định",
                Quantity = od.Quantity,
                UnitPrice = od.HistoricalPrice
            }).ToList()
        }).ToList();
    }
}