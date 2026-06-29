using MediatR;

namespace RestaurantManagement.Application.Members.Commands.ApplyPoints;

public class ApplyPointsCommand : IRequest<bool>
{
    public string CardId { get; set; }
    public int PointsToRedeem { get; set; }

    public ApplyPointsCommand(string cardId, int pointsToRedeem)
    {
        CardId = cardId;
        PointsToRedeem = pointsToRedeem;
    }
}