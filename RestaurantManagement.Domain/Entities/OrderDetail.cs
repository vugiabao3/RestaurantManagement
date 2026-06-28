using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities
{
    [Table("Order_Details")]
    public class OrderDetail
    {
        [Key]
        [Column("order_detail_id")]
        public int OrderDetailId { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order? Order { get; set; }

        [Column("menu_item_id")]
        public int MenuItemId { get; set; }

        [ForeignKey(nameof(MenuItemId))]
        public MenuItem? MenuItem { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("unit_price", TypeName = "decimal(19,2)")]
        public decimal UnitPrice { get; set; }

        [Column("status")]
        [MaxLength(50)]
        public string Status { get; set; } = OrderStatus.Pending;

        [Column("note")]
        [MaxLength(500)]
        public string? Note { get; set; }

        /// <summary>
        /// BR04 (UC_KIT_02): Optimistic Locking - dùng làm Concurrency Token
        /// để tránh ghi đè khi nhiều thiết bị bếp cùng cập nhật 1 món.
        /// </summary>
        [Column("version")]
        [ConcurrencyCheck]
        public int Version { get; set; } = 1;

        /// <summary>
        /// Mở rộng ngoài ERD gốc: lưu trạng thái trước đó để phục vụ
        /// chức năng "Hoàn tác" (Undo) trong UC_KIT_02 - Alternative Flow.
        /// </summary>
        [Column("previous_status")]
        [MaxLength(50)]
        public string? PreviousStatus { get; set; }

        public decimal GetSubtotal()
        {
            return UnitPrice * Quantity;
        }
    }
}
