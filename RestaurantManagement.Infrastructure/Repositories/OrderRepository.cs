using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces.Repositories; // Đảm bảo đúng đường dẫn interface của bạn
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository // Kế thừa interface
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<CustomerOrder> GetQueryable()
    {
        return _context.CustomerOrders;
    }

    public async Task AddAsync(CustomerOrder order, CancellationToken cancellationToken) 
    { 
        await _context.CustomerOrders.AddAsync(order, cancellationToken); 
    }

    public void Update(CustomerOrder order)
    {
        _context.CustomerOrders.Update(order);
    }

    // Hàm lấy danh sách đơn hàng đã thanh toán theo tháng
    public async Task<IEnumerable<CustomerOrder>> GetPaidOrdersByMonthAsync(int month, int year)
{
    return await _context.CustomerOrders 
        .Include(o => o.OrderDetails)    
        .ThenInclude(od => od.MenuItem) // Include MenuItem để lấy tên món ăn phục vụ cho báo cáo
        .Include(o => o.Invoice)         // BẮT BUỘC Include Invoice để lấy thời gian và số tiền thanh toán
        .Where(o => o.OrderStatus == OrderStatus.Paid // Đổi từ chuỗi "Paid" sang đúng Enum OrderStatus.Paid
                 && o.Invoice != null 
                 && o.Invoice.PaymentTime.Month == month 
                 && o.Invoice.PaymentTime.Year == year) // Đổi từ thuộc tính lỗi sang Invoice.PaymentTime
        .ToListAsync();
}

    // HÀM ĐƯỢC BỔ SUNG ĐỂ SỬA LỖI CS0535
    public async Task<CustomerOrder?> GetOrderForPaymentAsync(int orderId, CancellationToken cancellationToken)
    {
        return await _context.CustomerOrders
            .Include(o => o.DiningTable)
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.MenuItem)
            .FirstOrDefaultAsync(o => o.OrderId == orderId, cancellationToken);
    }
}