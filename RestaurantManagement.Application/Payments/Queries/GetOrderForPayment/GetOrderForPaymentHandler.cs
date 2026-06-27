using MediatR;
using RestaurantManagement.Application.Interfaces.Repositories;
using RestaurantManagement.Application.Payments.DTOs;
using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Application.Payments.Queries.GetOrderForPayment;

public class GetOrderForPaymentHandler : IRequestHandler<GetOrderForPaymentQuery, OrderPaymentSummaryDto>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderForPaymentHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderPaymentSummaryDto> Handle(GetOrderForPaymentQuery request, CancellationToken cancellationToken)
    {
        // 1. Lấy dữ liệu từ DB
        var order = await _orderRepository.GetOrderForPaymentAsync(request.OrderId, cancellationToken);

        if (order == null || order.OrderStatus != OrderStatus.Unpaid)
        {
            throw new Exception($"Không tìm thấy đơn hàng {request.OrderId} hoặc đơn đã thanh toán.");
        }

        // 2. Map sang DTO để trả về cho giao diện (Figma: Màn hình chi tiết thanh toán)
        var dto = new OrderPaymentSummaryDto
        {
            OrderId = order.OrderId,
            TableNumber = order.DiningTable?.TableNumber ?? "Mang về",
            TotalAmount = order.OrderDetails.Sum(od => od.Quantity * od.HistoricalPrice),
            Items = order.OrderDetails.Select(od => new OrderItemDto
            {
                ItemName = od.MenuItem?.ItemName ?? "Unknown",
                Quantity = od.Quantity,
                Price = od.HistoricalPrice
            }).ToList()
        };

        return dto;
    }
}