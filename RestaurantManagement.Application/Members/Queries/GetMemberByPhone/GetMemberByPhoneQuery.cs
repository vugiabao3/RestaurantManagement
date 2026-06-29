using MediatR;
using RestaurantManagement.Application.Members.DTOs;

namespace RestaurantManagement.Application.Members.Queries.GetMemberByPhone;

public class GetMemberByPhoneQuery : IRequest<MemberDto?>
{
    public string PhoneNumber { get; set; }

    public GetMemberByPhoneQuery(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }
}