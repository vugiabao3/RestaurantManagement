using MediatR;

namespace RestaurantManagement.Application.KitchenAreas.Commands.DeleteKitchenArea
{
    public class DeleteKitchenAreaCommand : IRequest<string>
    {
        public int KitchenAreaId { get; set; }
    }
}
