using MediatR;
using RestaurantManagement.Application.KitchenAreas;

namespace RestaurantManagement.Application.KitchenAreas.Queries.GetKitchenAreas
{
    public class GetKitchenAreasQuery : IRequest<List<KitchenAreaDto>> { }
}
