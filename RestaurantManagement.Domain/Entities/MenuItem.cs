using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities
{
    [Table("Menu_Items")]
    public class MenuItem
    {
        [Key]
        [Column("menu_item_id")]
        public int MenuItemId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("category")]
        public string Category { get; set; } = string.Empty;

        [Column("price", TypeName = "decimal(19,2)")]
        public decimal Price { get; set; }

        [Column("is_available")]
        public bool IsAvailable { get; set; } = true;

        [MaxLength(500)]
        [Column("image_url")]
        public string? ImageUrl { get; set; }

        [Column("cooking_time_standard")]
        public int CookingTimeStandard { get; set; }

        [Column("created")]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [Column("kitchen_area_id")]
        public int? KitchenAreaId { get; set; }

        public KitchenArea? KitchenArea { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
            = new List<OrderItem>();

        public ICollection<OrderDetail> OrderDetails { get; set; }
            = new List<OrderDetail>();
    }
}
