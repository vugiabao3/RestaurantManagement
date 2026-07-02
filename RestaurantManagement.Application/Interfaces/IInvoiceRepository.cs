using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces;

public interface IInvoiceRepository
{
    Task AddAsync(Invoice invoice);

    Task<Invoice?> GetByIdAsync(int invoiceId);

    Task<List<Invoice>> GetByOrderIdAsync(int orderId);

    Task<List<Invoice>> GetAllAsync();
    // Lấy hóa đơn theo năm
    Task<List<Invoice>> GetInvoicesByYearAsync(int year);
}