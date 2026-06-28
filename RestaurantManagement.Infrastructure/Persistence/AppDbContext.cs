using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

	public DbSet<Category> Categories { get; set; }
	public DbSet<User> User { get; set; }
	public DbSet<RevenueReport> RevenueReports { get; set; }
	public DbSet<Dish> Dishes { get; set; }
	public DbSet<Menu> Menus { get; set; }


	public DbSet<RestaurantTable> RestaurantTables { get; set; }
	public DbSet<AppUser> Users { get; set; }
	public DbSet<MenuItem> MenuItems { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }
	public DbSet<Notification> Notifications { get; set; }
	public DbSet<KitchenDelayLog> KitchenDelayLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RestaurantTable>(entity =>
            {
                entity.ToTable("restaurant_table");
                entity.HasKey(e => e.TableId);
                entity.Property(e => e.TableId).HasColumnName("table_id");
                entity.Property(e => e.TableNumber).HasColumnName("table_number").HasMaxLength(10);
                entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(20);
                entity.Property(e => e.Capacity).HasColumnName("capacity");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Username).HasColumnName("username").HasMaxLength(50);
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash").HasMaxLength(255);
                entity.Property(e => e.FullName).HasColumnName("full_name").HasMaxLength(100);
                entity.Property(e => e.Role).HasColumnName("role").HasMaxLength(20);
                entity.Property(e => e.IsActive).HasColumnName("is_active");
                entity.Property(e => e.CreateAt).HasColumnName("create_at");
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.ToTable("menu_item");
                entity.HasKey(e => e.ItemId);
                entity.Property(e => e.ItemId).HasColumnName("item_id");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100);
                entity.Property(e => e.ImageUrl).HasColumnName("image_url").HasMaxLength(255);
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(12,0)");
                entity.Property(e => e.CookingTimeStandard).HasColumnName("cooking_time_standard");
                entity.Property(e => e.Category).HasColumnName("category").HasMaxLength(50);
                entity.Property(e => e.IsAvailable).HasColumnName("is_available");
                entity.Property(e => e.CreateAt).HasColumnName("create_at");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.HasKey(e => e.OrderId);
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.TableId).HasColumnName("table_id");
                entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(10);
                entity.Property(e => e.CreateAt).HasColumnName("create_at");
                entity.Property(e => e.ClosedAt).HasColumnName("closed_at");

                entity.HasOne(e => e.Table)
                    .WithMany(t => t.Orders)
                    .HasForeignKey(e => e.TableId);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_item");
                entity.HasKey(e => e.OrderItemId);
                entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.ItemId).HasColumnName("item_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.OriginalPrice).HasColumnName("original_price").HasColumnType("decimal(12,0)");
                entity.Property(e => e.DiscountRate).HasColumnName("discount_rate").HasColumnType("decimal(5,2)");
                entity.Property(e => e.UnitPrice).HasColumnName("unit_price").HasColumnType("decimal(12,0)");
                entity.Property(e => e.ItemStatus).HasColumnName("item_status").HasMaxLength(10);
                entity.Property(e => e.IsDelayed).HasColumnName("is_delayed");
                entity.Property(e => e.OrderedAt).HasColumnName("ordered_at");
                entity.Property(e => e.CompletedAt).HasColumnName("completed_at");

                entity.HasOne(e => e.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(e => e.OrderId);

                entity.HasOne(e => e.MenuItem)
                    .WithMany(m => m.OrderItems)
                    .HasForeignKey(e => e.ItemId);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification");
                entity.HasKey(e => e.NotificationId);
                entity.Property(e => e.NotificationId).HasColumnName("notification_id");
                entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
                entity.Property(e => e.TableId).HasColumnName("table_id");
                entity.Property(e => e.Content).HasColumnName("content").HasMaxLength(500);
                entity.Property(e => e.IsDisplayed).HasColumnName("is_displayed");
                entity.Property(e => e.QueueStatus).HasColumnName("queue_status").HasMaxLength(10);
                entity.Property(e => e.CreateAt).HasColumnName("create_at");
                entity.Property(e => e.DisplayedAt).HasColumnName("displayed_at");

                entity.HasOne(e => e.OrderItem)
                    .WithOne(oi => oi.Notification)
                    .HasForeignKey<Notification>(e => e.OrderItemId);

                entity.HasOne(e => e.Table)
                    .WithMany()
                    .HasForeignKey(e => e.TableId);
            });

            modelBuilder.Entity<KitchenDelayLog>(entity =>
            {
                entity.ToTable("kitchen_delay_log");
                entity.HasKey(e => e.LogId);
                entity.Property(e => e.LogId).HasColumnName("log_id");
                entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
                entity.Property(e => e.ChefUserId).HasColumnName("chef_user_id");
                entity.Property(e => e.DelayReason).HasColumnName("delay_reason").HasMaxLength(30);
                entity.Property(e => e.DelayPriority).HasColumnName("delay_priority").HasMaxLength(10);
                entity.Property(e => e.DelayDuration).HasColumnName("delay_duration");
                entity.Property(e => e.DelayNotes).HasColumnName("delay_notes").HasMaxLength(255);
                entity.Property(e => e.IsAutoDetected).HasColumnName("is_auto_detected");
                entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(10);
                entity.Property(e => e.StartedAt).HasColumnName("started_at");
                entity.Property(e => e.ResolvedAt).HasColumnName("resolved_at");

                entity.HasOne(e => e.OrderItem)
                    .WithMany(oi => oi.DelayLogs)
                    .HasForeignKey(e => e.OrderItemId);

                entity.HasOne(e => e.ChefUser)
                    .WithMany()
                    .HasForeignKey(e => e.ChefUserId);
            });
        }
    }
}
