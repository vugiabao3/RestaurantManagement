using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities
{
    [Table("Audit_Logs")]
    public class AuditLog
    {
        [Key]
        [Column("audit_log_id")]
        public long AuditLogId { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Required, MaxLength(100)]
        [Column("action")]
        public string Action { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        [Column("entity_name")]
        public string EntityName { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("entity_id")]
        public string? EntityId { get; set; }

        [MaxLength(1000)]
        [Column("description")]
        public string? Description { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [MaxLength(64)]
        [Column("ip_address")]
        public string? IpAddress { get; set; }
    }
}
