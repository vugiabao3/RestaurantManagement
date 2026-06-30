namespace RestaurantManagement.Application.Members.Commands.RegisterMember;

public class RegisterMemberResponse
{
    public int MemberId { get; set; }

    public string CardId { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
}