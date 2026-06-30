using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces;

public interface ITableRepository
{
    Task<DiningTable?> GetByIdAsync(int tableId);

    Task UpdateAsync(DiningTable table);

    Task<List<DiningTable>> GetAvailableTablesAsync();

    Task<List<DiningTable>> GetOccupiedTablesAsync();
}