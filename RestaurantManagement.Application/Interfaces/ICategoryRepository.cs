using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();

        Task<bool> ExistsByNameAsync(string name);

        Task AddAsync(Category category);

        Task<Category?> GetByIdAsync(int id);

        Task UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<List<Category>> SearchByNameAsync(string keyword);

        Task<List<Dish>> GetDishesByCategoryIdAsync(
    int categoryId
);

    }
}