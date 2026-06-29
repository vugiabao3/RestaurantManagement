using MediatR;
using RestaurantManagement.Application.Members.DTOs;

namespace RestaurantManagement.Application.Members.Queries;
public class GetMemberByCardIdQuery : IRequest<MemberDto?>
{
    public string CardId { get; set; }

    public GetMemberByCardIdQuery(string cardId)
    {
        CardId = cardId;
    }
}