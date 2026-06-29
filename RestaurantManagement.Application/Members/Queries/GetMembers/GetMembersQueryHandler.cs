using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Members.DTOs;

namespace RestaurantManagement.Application.Members.Queries.GetMembers;

public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, List<MemberDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMembersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<MemberDto>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.MemberRepository.GetQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(m => m.FullName.Contains(request.SearchTerm) || m.PhoneNumber.Contains(request.SearchTerm));
        }

        return await query.Select(m => new MemberDto
        {
            MemberId = m.MemberId,
            FullName = m.FullName,
            PhoneNumber = m.PhoneNumber,
            LoyaltyPoints = m.LoyaltyPoints,
            CardId = m.CardId
        }).ToListAsync(cancellationToken);
    }
}