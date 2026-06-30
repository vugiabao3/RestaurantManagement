using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> User { get; set; }



        public DbSet<RevenueReport> RevenueReports { get; set; }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems
        {
            get;
            set;
        }
        public DbSet<MemberCard> MemberCards { get; set; }

        public DbSet<DiningTable> DiningTables { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DiningTable>()
                .HasKey(x => x.TableId);
        }
    }
}
