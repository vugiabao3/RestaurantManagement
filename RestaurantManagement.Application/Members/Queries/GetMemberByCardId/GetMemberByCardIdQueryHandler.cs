using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Members.DTOs;

namespace RestaurantManagement.Application.Members.Queries;
public class GetMemberByCardIdQueryHandler : IRequestHandler<GetMemberByCardIdQuery, MemberDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMemberByCardIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MemberDto?> Handle(GetMemberByCardIdQuery request, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MemberRepository.GetByCardIdAsync(request.CardId, cancellationToken);
        
        if (member == null) return null;

        // Tự động map sang MemberDto dùng chung của hệ thống
        return new MemberDto
        {
            MemberId = member.MemberId,
            FullName = member.FullName,
            PhoneNumber = member.PhoneNumber,
            LoyaltyPoints = member.LoyaltyPoints
        };
    }
}