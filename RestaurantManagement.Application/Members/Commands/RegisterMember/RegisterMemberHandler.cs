using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Members.Commands.RegisterMember;

public class RegisterMemberHandler
    : IRequestHandler<RegisterMemberCommand, RegisterMemberResponse>
{
    private readonly IMemberRepository _memberRepo;

    public RegisterMemberHandler(IMemberRepository memberRepo)
    {
        _memberRepo = memberRepo;
    }

    public async Task<RegisterMemberResponse> Handle(
        RegisterMemberCommand request,
        CancellationToken cancellationToken)
    {
        var existing = await _memberRepo
            .GetByPhoneAsync(request.PhoneNumber);

        if (existing != null)
        {
            return new RegisterMemberResponse
            {
                MemberId = existing.MemberId,
                CardId = existing.CardId ?? "",
                Message = "Member already exists"
            };
        }

        var member = new MemberCard
        {
            UserId = request.UserId ?? 0,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            LoyaltyPoints = 0,
            CardId = GenerateCardId()
        };

        await _memberRepo.AddAsync(member);

        return new RegisterMemberResponse
        {
            MemberId = member.MemberId,
            CardId = member.CardId!,
            Message = "Register successful"
        };
    }

    private string GenerateCardId()
    {
        return "MB" + DateTime.Now.Ticks.ToString()[..6];
    }
}