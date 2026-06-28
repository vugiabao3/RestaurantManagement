using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RestaurantManagement.Application.MenuItems.Commands.UpdateMenuItem
{
    public class UpdateMenuItemCommand : IRequest<UpdateMenuItemResponse>
    {
        public int MenuItemId { get; set; }
        [Required, StringLength(255)] public string Name { get; set; } = string.Empty;
        [Required, StringLength(100)] public string Category { get; set; } = string.Empty;
        [Range(typeof(decimal), "0.01", "999999999", ParseLimitsInInvariantCulture = true)] public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public int? KitchenAreaId { get; set; }
    }
}

