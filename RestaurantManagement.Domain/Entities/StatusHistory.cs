using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities
{
    [Table("Status_Histories")]
    public class StatusHistory
    {
        [Key]
        [Column("status_history_id")]
        public int StatusHistoryId { get; set; }

        [Column("order_id")]
        public int? OrderId { get; set; }

        [Column("order_detail_id")]
        public int? OrderDetailId { get; set; }

        [Required, MaxLength(50)]
        [Column("old_status")]
        public string OldStatus { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        [Column("new_status")]
        public string NewStatus { get; set; } = string.Empty;

        [Column("changed_by_user_id")]
        public int? ChangedByUserId { get; set; }

        [Column("changed_at")]
        public DateTime ChangedAt { get; set; } = DateTime.Now;

        [MaxLength(500)]
        [Column("reason")]
        public string? Reason { get; set; }
    }
}
