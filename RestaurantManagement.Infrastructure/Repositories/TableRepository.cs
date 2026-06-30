using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories;

public class TableRepository : ITableRepository
{
    private readonly AppDbContext _context;

    public TableRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DiningTable?> GetByIdAsync(int tableId)
    {
        return await _context.DiningTables
            .FirstOrDefaultAsync(x => x.TableId == tableId);
    }

    public async Task UpdateAsync(DiningTable table)
    {
        _context.DiningTables.Update(table);
        await _context.SaveChangesAsync();
    }

    public async Task<List<DiningTable>> GetAvailableTablesAsync()
    {
        return await _context.DiningTables
            .Where(x => x.CurrentStatus == TableStatus.Available)
            .ToListAsync();
    }

    public async Task<List<DiningTable>> GetOccupiedTablesAsync()
    {
        return await _context.DiningTables
            .Where(x => x.CurrentStatus == TableStatus.Occupied)
            .ToListAsync();
    }
}