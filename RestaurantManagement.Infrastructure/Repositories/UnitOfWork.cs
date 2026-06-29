using System;
using System.Threading;
using System.Threading.Tasks;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Interfaces.Repositories;
using RestaurantManagement.Infrastructure.Persistence;
using RestaurantManagement.Infrastructure.Repositories;

namespace RestaurantManagement.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    private IOrderRepository? _orderRepository;
    private IInvoiceRepository? _invoiceRepository;
    private IMemberRepository? _memberRepository; 
    private IShiftRepository? _shiftRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Orders = new OrderRepository(_context);
    }

    public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_context);
    public IInvoiceRepository InvoiceRepository => _invoiceRepository ??= new InvoiceRepository(_context);    
    public IMemberRepository MemberRepository => _memberRepository ??= new MemberRepository(_context);
    public IShiftRepository ShiftRepository => _shiftRepository ??= new ShiftRepository(_context);
    public IOrderRepository Orders { get; } 
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}