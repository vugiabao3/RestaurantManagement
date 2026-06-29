using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Members.Commands.ApplyPoints;

public class ApplyPointsCommandHandler : IRequestHandler<ApplyPointsCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ApplyPointsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ApplyPointsCommand request, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MemberRepository.GetByCardIdAsync(request.CardId, cancellationToken);
        
        if (member == null)
            throw new Exception("Không tìm thấy thẻ thành viên này.");

        if (member.LoyaltyPoints < request.PointsToRedeem)
            throw new Exception("Số điểm tích lũy hiện tại không đủ để thực hiện giao dịch.");

        // Thực hiện trừ điểm của thành viên
        member.LoyaltyPoints -= request.PointsToRedeem;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}