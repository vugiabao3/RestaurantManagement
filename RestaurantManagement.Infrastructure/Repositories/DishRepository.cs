using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Repositories;

public class DishRepository
    : IDishRepository
{
    private readonly AppDbContext _context;

    public DishRepository(
        AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Dish dish)
    {
        await _context.Dishes.AddAsync(dish);

        await _context.SaveChangesAsync();
    }

    public async Task<Dish?> GetByIdAsync(int id)
    {
        return await _context.Dishes
            .FindAsync(id);
    }
    public async Task UpdateAsync(Dish dish)
    {
        _context.Dishes.Update(dish);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Dish dish)
    {
        _context.Dishes.Remove(dish);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Dish>> GetAllAsync()
    {
        return await _context.Dishes
            .ToListAsync();
    }
    public async Task<List<Dish>> SearchAsync(
    string keyword)
    {
        return await _context.Dishes
            .Where(x =>
                x.Name.Contains(keyword))
            .ToListAsync();
    }
}
