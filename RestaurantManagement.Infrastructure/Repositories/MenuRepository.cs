using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Infrastructure.Persistence;

namespace RestaurantManagement.Infrastructure.Repositories;

public class MenuRepository
    : IMenuRepository
{
    private readonly AppDbContext _context;

    public MenuRepository(
        AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Menu menu)
    {
        await _context.Menus.AddAsync(menu);

        await _context.SaveChangesAsync();
    }

    public async Task<Menu?> GetByIdAsync(
        int id)
    {
        return await _context.Menus
            .Include(x => x.Categories)
            .FirstOrDefaultAsync(
                x => x.MenuId == id);
    }

    public async Task UpdateAsync(Menu menu)
    {
        _context.Menus.Update(menu);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Menu menu)
    {
        _context.Menus.Remove(menu);

        await _context.SaveChangesAsync();
    }
    public async Task<List<Category>>
 GetCategoriesByMenuAsync(int menuId)
    {
        return await _context.Categories
            .Where(c => c.MenuId == menuId)
            .ToListAsync();
    }


    public async Task<List<Menu>> GetAllAsync()
    {
        return await _context.Menus
            .Include(x => x.Categories)
            .ToListAsync();
    }
}