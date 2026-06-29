using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Application.Interfaces; 

namespace RestaurantManagement.Application.Payments.Queries.GetOrderForPayment;

public class GetOrderForPaymentQueryHandler : IRequestHandler<GetOrderForPaymentQuery, OrderSummaryDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderForPaymentQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderSummaryDto> Handle(GetOrderForPaymentQuery request, CancellationToken cancellationToken)
    {
        // 1. Tìm Order đang Unpaid dựa trên TableNumber (Theo BR01)
        // Lưu ý: Cần Include OrderDetails và MenuItem để lấy tên & giá món
        var order = await _unitOfWork.OrderRepository
            .GetQueryable()
            .Include(o => o.DiningTable)
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.MenuItem)
            .FirstOrDefaultAsync(o => 
                o.DiningTable != null && // Sửa lỗi CS8602: Kiểm tra null trước khi truy cập TableNumber
                o.DiningTable.TableNumber == request.TableNumber && 
                o.OrderStatus == OrderStatus.Unpaid, cancellationToken);

        // 2. Xử lý Alternative Flow 1a: Không tìm thấy hóa đơn
        if (order == null)
        {
            throw new Exception("MS01: Mã bàn không tồn tại hoặc bàn này hiện không có hóa đơn cần thanh toán.");
        }

        // 3. Map dữ liệu sang DTO và tính toán tổng tiền (Theo BR02)
        // Sử dụng Null-conditional operator (?.) và Null-coalescing operator (??) để đảm bảo an toàn 100%
        var dto = new OrderSummaryDto
        {
            OrderId = order.OrderId,
            TableNumber = order.DiningTable?.TableNumber ?? string.Empty,
            Items = order.OrderDetails?.Select(od => new OrderItemDto
            {
                ItemName = od.MenuItem?.ItemName ?? "Món không xác định",
                Quantity = od.Quantity,
                UnitPrice = od.HistoricalPrice // Dùng giá lịch sử lúc gọi món
            }).ToList() ?? new List<OrderItemDto>()
        };

        // 4. Tính toán tổng tiền hóa đơn an toàn
        dto.TotalAmount = dto.Items.Sum(i => i.SubTotal); 

        return dto;
    }
}