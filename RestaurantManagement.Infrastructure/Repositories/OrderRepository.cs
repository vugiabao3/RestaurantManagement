using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Common.Exceptions;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Order>> GetPendingOrdersAsync(int? kitchenAreaId = null)
        {
            var query = _context.Orders
                .AsNoTracking()
                .Where(o => o.Status == OrderStatus.Pending || o.Status == OrderStatus.Preparing)
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.MenuItem)
                        .ThenInclude(m => m!.KitchenArea)
                .AsQueryable();

            if (kitchenAreaId.HasValue)
            {
                query = query.Where(o => o.OrderDetails.Any(d => d.MenuItem!.KitchenAreaId == kitchenAreaId.Value));
            }

            return query
                .OrderByDescending(o => o.Priority)
                .ThenBy(o => o.Created)
                .ThenBy(o => o.OrderId)
                .ToListAsync();
        }

        public Task<Order?> GetOrderWithDetailsAsync(int orderId) =>
            _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.MenuItem)
                        .ThenInclude(m => m!.KitchenArea)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

        public Task<OrderDetail?> GetOrderDetailWithContextAsync(int orderDetailId) =>
            _context.OrderDetails
                .Include(d => d.MenuItem)
                    .ThenInclude(m => m!.KitchenArea)
                .Include(d => d.Order)
                    .ThenInclude(o => o!.OrderDetails)
                .FirstOrDefaultAsync(d => d.OrderDetailId == orderDetailId);

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ConflictException("Dữ liệu đang được xử lý bởi máy khác, vui lòng làm mới");
            }
        }
    }
}
