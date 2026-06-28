using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("order_number")]
        public int OrderNumber { get; set; }

        [Column("table_id")]
        public int TableId { get; set; }

        [Column("table_number")]
        public int TableNumber { get; set; }

        [Required, MaxLength(50)]
        [Column("status")]
        public string Status { get; set; } = OrderStatus.Pending;

        [Column("total_amount", TypeName = "decimal(19,2)")]
        public decimal TotalAmount { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("completed")]
        public DateTime? Completed { get; set; }

        [MaxLength(500)]
        [Column("cancel_reason")]
        public string? CancelReason { get; set; }

        [Column("priority")]
        public int Priority { get; set; }

        [Column("version")]
        [ConcurrencyCheck]
        public int Version { get; set; } = 1;

        public RestaurantTable? Table { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
            = new List<OrderItem>();

        public ICollection<OrderDetail> OrderDetails { get; set; }
            = new List<OrderDetail>();

        public bool IsAllItemsReady()
        {
            return OrderDetails.Count > 0 &&
                   OrderDetails.All(
                       detail => detail.Status == OrderStatus.Ready
                   );
        }
    }
}