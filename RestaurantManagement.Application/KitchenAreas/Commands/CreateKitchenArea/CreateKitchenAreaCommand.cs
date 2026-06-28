using System.ComponentModel.DataAnnotations;
using MediatR;
using RestaurantManagement.Application.KitchenAreas;

namespace RestaurantManagement.Application.KitchenAreas.Commands.CreateKitchenArea
{
    public class CreateKitchenAreaCommand : IRequest<KitchenAreaDto>
    {
        [Required, StringLength(100)] public string Name { get; set; } = string.Empty;
        [StringLength(255)] public string? Description { get; set; }
    }
}
