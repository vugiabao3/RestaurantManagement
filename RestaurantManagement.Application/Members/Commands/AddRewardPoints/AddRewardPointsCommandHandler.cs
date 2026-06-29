using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Members.Commands;
public class AddRewardPointsCommandHandler : IRequestHandler<AddRewardPointsCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddRewardPointsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AddRewardPointsCommand request, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MemberRepository.GetByIdAsync(request.MemberId, cancellationToken);
        
        if (member != null)
        {
            // Xử lý Business Rule BR01 của UC_PAY_03:
            // Mỗi 100.000 VNĐ chi tiêu sẽ được cộng 1 điểm thưởng (Lấy phần nguyên)
            int pointsEarned = (int)(request.InvoiceAmount / 100000);

            if (pointsEarned > 0)
            {
                member.LoyaltyPoints += pointsEarned;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
        
        return false;
    }
}