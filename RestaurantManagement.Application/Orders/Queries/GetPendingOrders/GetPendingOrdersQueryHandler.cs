using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Orders.DTOs;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Orders.Queries.GetPendingOrders;

public class GetPendingOrdersQueryHandler : IRequestHandler<GetPendingOrdersQuery, List<OrderDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPendingOrdersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<OrderDto>> Handle(GetPendingOrdersQuery request, CancellationToken cancellationToken)
    {
        // 1. Lấy danh sách các Order đang Unpaid
        var orders = await _unitOfWork.OrderRepository.GetQueryable()
            .Include(o => o.DiningTable)
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.MenuItem)
            .Where(o => o.OrderStatus == OrderStatus.Unpaid)
            .OrderByDescending(o => o.StartTime) // Đơn mới nhất xếp lên đầu
            .ToListAsync(cancellationToken);

        // 2. Map sang OrderDto
        return orders.Select(o => new OrderDto
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