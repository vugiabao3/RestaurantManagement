using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces;

public interface IDiningTableRepository
{
    Task<List<DiningTable>> GetAllAsync();

    Task<List<DiningTable>> GetAvailableTablesAsync();

    Task<DiningTable?> GetByIdAsync(int tableId);

    Task UpdateAsync(DiningTable table);
}