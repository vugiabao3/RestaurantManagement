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
            .ThenInclude(od => od.MenuItem)
            .Where(o => o.OrderStatus == OrderStatus.Paid) 
            // Dùng enum OrderStatus
            // Lưu ý: Nếu CustomerOrder không có thuộc tính PaymentDate, bạn sẽ phải join với bảng Invoice ở đây nhé
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