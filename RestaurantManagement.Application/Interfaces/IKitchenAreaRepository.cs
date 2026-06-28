using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces
{
    public interface IKitchenAreaRepository
    {
        Task<List<KitchenArea>> GetAllAsync();
        Task<KitchenArea?> GetByIdAsync(int id);
        Task<bool> NameExistsAsync(string name, int? excludeId = null);
        Task AddAsync(KitchenArea area);
        Task DeleteAsync(KitchenArea area);
        Task SaveChangesAsync();
    }
}
