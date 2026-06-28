using System.ComponentModel.DataAnnotations;
using MediatR;
using RestaurantManagement.Application.KitchenAreas;

namespace RestaurantManagement.Application.KitchenAreas.Commands.UpdateKitchenArea
{
    public class UpdateKitchenAreaCommand : IRequest<KitchenAreaDto>
    {
        public int KitchenAreaId { get; set; }
        [Required, StringLength(100)] public string Name { get; set; } = string.Empty;
        [StringLength(255)] public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
