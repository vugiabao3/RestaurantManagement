using System;
using System.Collections.Generic;
using MediatR;
using RestaurantManagement.Application.Orders.DTOs;

namespace RestaurantManagement.Application.Orders.Queries.GetCompletedOrders;

public class GetCompletedOrdersQuery : IRequest<List<OrderDto>>
{
    // Tùy chọn: Lọc theo ngày để tránh tải toàn bộ dữ liệu lịch sử gây chậm hệ thống
    public DateTime? FilterDate { get; set; } 

    public GetCompletedOrdersQuery(DateTime? filterDate = null)
    {
        FilterDate = filterDate;
    }
}