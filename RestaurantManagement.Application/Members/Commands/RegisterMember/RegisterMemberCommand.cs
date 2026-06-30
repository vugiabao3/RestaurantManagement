using MediatR;

namespace RestaurantManagement.Application.Members.Commands.RegisterMember;

public class RegisterMemberCommand : IRequest<RegisterMemberResponse>
{
    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public int? UserId { get; set; }
}