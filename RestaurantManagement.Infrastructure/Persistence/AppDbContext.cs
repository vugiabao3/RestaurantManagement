using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace RestaurantManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // ==========================================
        // MODULE ADMIN (FILE GỐC)
        // ==========================================
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RevenueReport> RevenueReports { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        // ==========================================
        // MODULE THU NGÂN (BỔ SUNG)
        // ==========================================
        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<MemberCard> MemberCards { get; set; }
        public DbSet<DiningTable> DiningTables { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        // ==========================================
        // CẤU HÌNH DATABASE
        // ==========================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Tự động apply các file cấu hình entity (nếu có)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}