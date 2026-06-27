namespace RestaurantManagement.Domain.Entities;

public class MemberCard
{
    public int MemberId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int LoyaltyPoints { get; set; }
    public string? CardId { get; set; } // RFID or Physical ID

    // Relationship
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}