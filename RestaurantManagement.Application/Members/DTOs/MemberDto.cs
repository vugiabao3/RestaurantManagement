namespace RestaurantManagement.Application.Members.DTOs;

public class MemberDto
{
    public int MemberId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int LoyaltyPoints { get; set; }
    public string? CardId { get; set; }
}