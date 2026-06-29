using System.Collections.Generic;
using MediatR;
using RestaurantManagement.Application.Orders.DTOs;

namespace RestaurantManagement.Application.Orders.Queries.GetPendingOrders;

public class GetPendingOrdersQuery : IRequest<List<OrderDto>>
{
    // Lấy tất cả đơn hàng đang Unpaid nên không cần tham số đầu vào
}