using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _context;

    public InvoiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Invoice invoice)
    {
        await _context.Invoices.AddAsync(invoice);
        await _context.SaveChangesAsync();
    }

    public async Task<Invoice?> GetByIdAsync(int invoiceId)
    {
        return await _context.Invoices
            .Include(x => x.Order)
            .Include(x => x.MemberCard)
            .FirstOrDefaultAsync(x => x.InvoiceId == invoiceId);
    }

    public async Task<List<Invoice>> GetByOrderIdAsync(int orderId)
    {
        return await _context.Invoices
            .Where(x => x.OrderId == orderId)
            .ToListAsync();
    }

    public async Task<List<Invoice>> GetAllAsync()
    {
        return await _context.Invoices
            .Include(x => x.Order)
            .ToListAsync();
    }
}