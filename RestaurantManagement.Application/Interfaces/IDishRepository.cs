using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces;

public interface IDishRepository
{
    Task AddAsync(Dish dish);
    Task<Dish?> GetByIdAsync(int id);

    Task UpdateAsync(Dish dish);

    Task DeleteAsync(Dish dish);
    Task<List<Dish>> GetAllAsync();

    Task<List<Dish>> SearchAsync(string keyword);
}