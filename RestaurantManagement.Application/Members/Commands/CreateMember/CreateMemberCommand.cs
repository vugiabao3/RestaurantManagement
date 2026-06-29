using MediatR;

namespace RestaurantManagement.Application.Members.Commands.CreateMember;

public class CreateMemberCommand : IRequest<int>
{
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? CardId { get; set; }

    public CreateMemberCommand(string fullName, string phoneNumber, string? cardId)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        CardId = cardId;
    }
}