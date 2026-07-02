using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories;

public class DiningTableRepository : IDiningTableRepository
{
    private readonly AppDbContext _context;

    public DiningTableRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<DiningTable>> GetAllAsync()
    {
        return await _context.DiningTables
            .OrderBy(t => t.TableNumber)
            .ToListAsync();
    }

    public async Task<List<DiningTable>> GetAvailableTablesAsync()
    {
        return await _context.DiningTables
            .Where(t => t.CurrentStatus == TableStatus.Available)
            .OrderBy(t => t.TableNumber)
            .ToListAsync();
    }

    public async Task<DiningTable?> GetByIdAsync(int tableId)
    {
        return await _context.DiningTables
            .FirstOrDefaultAsync(t => t.TableId == tableId);
    }

    public async Task UpdateAsync(DiningTable table)
    {
        _context.DiningTables.Update(table);

        await _context.SaveChangesAsync();
    }
}