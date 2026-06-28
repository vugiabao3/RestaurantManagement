using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Main module
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;
        public DbSet<RevenueReport> RevenueReports { get; set; } = null!;
        public DbSet<Dish> Dishes { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;

        // Kitchen Tracking module
        public DbSet<RestaurantTable> RestaurantTables { get; set; } = null!;
        public DbSet<AppUser> Users { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<KitchenDelayLog> KitchenDelayLogs { get; set; } = null!;

        // Kitchen V9 module
        public DbSet<MenuItem> MenuItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public DbSet<KitchenArea> KitchenAreas { get; set; } = null!;
        public DbSet<StatusHistory> StatusHistories { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Main module
            modelBuilder.Entity<Category>()
                .ToTable("Categories");

            modelBuilder.Entity<User>()
                .ToTable("User");

            modelBuilder.Entity<RevenueReport>(entity =>
            {
                entity.ToTable("RevenueReports");

                entity.Property(e => e.TotalRevenue)
                    .HasPrecision(19, 2);
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.ToTable("Dishes");

                entity.Property(e => e.Price)
                    .HasPrecision(19, 2);
            });

            modelBuilder.Entity<Menu>()
                .ToTable("Menus");

            modelBuilder.Entity<Admin>()
                .ToTable("ADMIN");

            // Kitchen Tracking: restaurant_table
            modelBuilder.Entity<RestaurantTable>(entity =>
            {
                entity.ToTable("restaurant_table");

                entity.HasKey(e => e.TableId);

                entity.Property(e => e.TableId)
                    .HasColumnName("table_id");

                entity.Property(e => e.TableNumber)
                    .HasColumnName("table_number")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.Capacity)
                    .HasColumnName("capacity");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at");
            });

            // Kitchen Tracking: users
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("password_hash")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.FullName)
                    .HasColumnName("full_name")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active");

                entity.Property(e => e.CreateAt)
                    .HasColumnName("create_at");
            });

            // Kitchen V9: Kitchen_Areas
            modelBuilder.Entity<KitchenArea>(entity =>
            {
                entity.ToTable("Kitchen_Areas");

                entity.HasKey(e => e.KitchenAreaId);

                entity.Property(e => e.KitchenAreaId)
                    .HasColumnName("kitchen_area_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active");
            });

            // Unified MenuItem for Tracking + V9
            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.ToTable("Menu_Items");

                entity.HasKey(e => e.MenuItemId);

                entity.Property(e => e.MenuItemId)
                    .HasColumnName("menu_item_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasPrecision(19, 2);

                entity.Property(e => e.IsAvailable)
                    .HasColumnName("is_available");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("image_url")
                    .HasMaxLength(500);

                entity.Property(e => e.CookingTimeStandard)
                    .HasColumnName("cooking_time_standard");

                entity.Property(e => e.Created)
                    .HasColumnName("created");

                entity.Property(e => e.KitchenAreaId)
                    .HasColumnName("kitchen_area_id");

                entity.HasOne(e => e.KitchenArea)
                    .WithMany(e => e.MenuItems)
                    .HasForeignKey(e => e.KitchenAreaId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Unified Order for Tracking + V9
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id");

                entity.Property(e => e.OrderNumber)
                    .HasColumnName("order_number");

                entity.Property(e => e.TableId)
                    .HasColumnName("table_id")
                    .IsRequired();

                entity.Property(e => e.TableNumber)
                    .HasColumnName("table_number");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.TotalAmount)
                    .HasColumnName("total_amount")
                    .HasPrecision(19, 2);

                entity.Property(e => e.Created)
                    .HasColumnName("created");

                entity.Property(e => e.Completed)
                    .HasColumnName("completed");

                entity.Property(e => e.CancelReason)
                    .HasColumnName("cancel_reason")
                    .HasMaxLength(500);

                entity.Property(e => e.Priority)
                    .HasColumnName("priority");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .IsConcurrencyToken();

                entity.HasOne(e => e.Table)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.TableId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Kitchen Tracking: order_item
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_item");

                entity.HasKey(e => e.OrderItemId);

                entity.Property(e => e.OrderItemId)
                    .HasColumnName("order_item_id");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity");

                entity.Property(e => e.OriginalPrice)
                    .HasColumnName("original_price")
                    .HasPrecision(19, 2);

                entity.Property(e => e.DiscountRate)
                    .HasColumnName("discount_rate")
                    .HasPrecision(5, 2);

                entity.Property(e => e.UnitPrice)
                    .HasColumnName("unit_price")
                    .HasPrecision(19, 2);

                entity.Property(e => e.ItemStatus)
                    .HasColumnName("item_status")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.IsDelayed)
                    .HasColumnName("is_delayed");

                entity.Property(e => e.OrderedAt)
                    .HasColumnName("ordered_at");

                entity.Property(e => e.CompletedAt)
                    .HasColumnName("completed_at");

                entity.HasOne(e => e.Order)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.MenuItem)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.ItemId)
                    .HasPrincipalKey(e => e.MenuItemId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Kitchen Tracking: notification
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification");

                entity.HasKey(e => e.NotificationId);

                entity.Property(e => e.NotificationId)
                    .HasColumnName("notification_id");

                entity.Property(e => e.OrderItemId)
                    .HasColumnName("order_item_id");

                entity.Property(e => e.TableId)
                    .HasColumnName("table_id");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(e => e.IsDisplayed)
                    .HasColumnName("is_displayed");

                entity.Property(e => e.QueueStatus)
                    .HasColumnName("queue_status")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.CreateAt)
                    .HasColumnName("create_at");

                entity.Property(e => e.DisplayedAt)
                    .HasColumnName("displayed_at");

                entity.HasOne(e => e.OrderItem)
                    .WithOne(e => e.Notification)
                    .HasForeignKey<Notification>(e => e.OrderItemId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Table)
                    .WithMany()
                    .HasForeignKey(e => e.TableId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Kitchen Tracking: kitchen_delay_log
            modelBuilder.Entity<KitchenDelayLog>(entity =>
            {
                entity.ToTable("kitchen_delay_log");

                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId)
                    .HasColumnName("log_id");

                entity.Property(e => e.OrderItemId)
                    .HasColumnName("order_item_id");

                entity.Property(e => e.ChefUserId)
                    .HasColumnName("chef_user_id");

                entity.Property(e => e.DelayReason)
                    .HasColumnName("delay_reason")
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(e => e.DelayPriority)
                    .HasColumnName("delay_priority")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.DelayDuration)
                    .HasColumnName("delay_duration");

                entity.Property(e => e.DelayNotes)
                    .HasColumnName("delay_notes")
                    .HasMaxLength(255);

                entity.Property(e => e.IsAutoDetected)
                    .HasColumnName("is_auto_detected");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.StartedAt)
                    .HasColumnName("started_at");

                entity.Property(e => e.ResolvedAt)
                    .HasColumnName("resolved_at");

                entity.HasOne(e => e.OrderItem)
                    .WithMany(e => e.DelayLogs)
                    .HasForeignKey(e => e.OrderItemId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ChefUser)
                    .WithMany()
                    .HasForeignKey(e => e.ChefUserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Kitchen V9: Order_Details
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("Order_Details");

                entity.HasKey(e => e.OrderDetailId);

                entity.Property(e => e.OrderDetailId)
                    .HasColumnName("order_detail_id");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id");

                entity.Property(e => e.MenuItemId)
                    .HasColumnName("menu_item_id");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity");

                entity.Property(e => e.UnitPrice)
                    .HasColumnName("unit_price")
                    .HasPrecision(19, 2);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(500);

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .IsConcurrencyToken();

                entity.Property(e => e.PreviousStatus)
                    .HasColumnName("previous_status")
                    .HasMaxLength(50);

                entity.HasOne(e => e.Order)
                    .WithMany(e => e.OrderDetails)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.MenuItem)
                    .WithMany(e => e.OrderDetails)
                    .HasForeignKey(e => e.MenuItemId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Kitchen V9: Status_Histories
            modelBuilder.Entity<StatusHistory>(entity =>
            {
                entity.ToTable("Status_Histories");

                entity.HasKey(e => e.StatusHistoryId);

                entity.Property(e => e.StatusHistoryId)
                    .HasColumnName("status_history_id");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id");

                entity.Property(e => e.OrderDetailId)
                    .HasColumnName("order_detail_id");

                entity.Property(e => e.OldStatus)
                    .HasColumnName("old_status")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.NewStatus)
                    .HasColumnName("new_status")
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.ChangedByUserId)
                    .HasColumnName("changed_by_user_id");

                entity.Property(e => e.ChangedAt)
                    .HasColumnName("changed_at");

                entity.Property(e => e.Reason)
                    .HasColumnName("reason")
                    .HasMaxLength(500);
            });

            // Kitchen V9: Audit_Logs
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("Audit_Logs");

                entity.HasKey(e => e.AuditLogId);

                entity.Property(e => e.AuditLogId)
                    .HasColumnName("audit_log_id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");

                entity.Property(e => e.Action)
                    .HasColumnName("action")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.EntityName)
                    .HasColumnName("entity_name")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.EntityId)
                    .HasColumnName("entity_id")
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(1000);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at");

                entity.Property(e => e.IpAddress)
                    .HasColumnName("ip_address")
                    .HasMaxLength(64);
            });
        }
    }
}
