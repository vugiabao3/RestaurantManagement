using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces.Repositories;

public interface IInvoiceRepository
{
    Task AddAsync(Invoice invoice, CancellationToken cancellationToken);
    Task<Invoice?> GetByIdAsync(int invoiceId, CancellationToken cancellationToken);
}