using MediatR;

namespace RestaurantManagement.Application.Members.Commands.DeleteMember;

public class DeleteMemberCommand : IRequest<bool>
{
    public int MemberId { get; set; }

    public DeleteMemberCommand(int memberId)
    {
        MemberId = memberId;
    }
}