using MediatR;

namespace RestaurantManagement.Application.Members.Commands.UpdateMember;

public class UpdateMemberCommand : IRequest<bool>
{
    public int MemberId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? CardId { get; set; }
}