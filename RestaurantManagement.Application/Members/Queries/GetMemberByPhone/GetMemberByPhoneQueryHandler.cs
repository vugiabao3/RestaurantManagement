using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Members.DTOs;

namespace RestaurantManagement.Application.Members.Queries.GetMemberByPhone;

public class GetMemberByPhoneQueryHandler : IRequestHandler<GetMemberByPhoneQuery, MemberDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMemberByPhoneQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MemberDto?> Handle(GetMemberByPhoneQuery request, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MemberRepository.GetByByPhoneNumberAsync(request.PhoneNumber, cancellationToken);
        
        if (member == null)
        {
            return null; // Frontend nhận null sẽ xử lý rẽ nhánh Alternative Flow 2a (Hiển thị MS08)
        }

        return new MemberDto
        {
            MemberId = member.MemberId,
            FullName = member.FullName,
            PhoneNumber = member.PhoneNumber,
            LoyaltyPoints = member.LoyaltyPoints,
            CardId = member.CardId
        };
    }
}