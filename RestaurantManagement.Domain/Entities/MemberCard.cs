using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities;

public class MemberCard
{
    [Key]
    public int MemberId { get; set; }
    public int UserId { get; set; }   // 👈 thêm cái này
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int LoyaltyPoints { get; set; }
    public string? CardId { get; set; }

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}