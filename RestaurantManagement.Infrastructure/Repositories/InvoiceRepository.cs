using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces.Repositories;
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

    public async Task AddAsync(Invoice invoice, CancellationToken cancellationToken)
    {
        await _context.Invoices.AddAsync(invoice, cancellationToken);
    }

    public async Task<Invoice?> GetByIdAsync(int invoiceId, CancellationToken cancellationToken)
    {
        return await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceId == invoiceId, cancellationToken);
    }
}