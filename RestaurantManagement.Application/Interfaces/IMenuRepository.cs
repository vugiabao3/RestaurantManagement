using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces;

public interface IMenuRepository
{
    Task AddAsync(Menu menu);

    Task<Menu?> GetByIdAsync(int id);

    Task UpdateAsync(Menu menu);

    Task DeleteAsync(Menu menu);

    Task<List<Category>> GetCategoriesByMenuAsync(int menuId);
    Task<List<Menu>> GetAllAsync();
}