using MediatR;

namespace RestaurantManagement.Application
    .Menus.Commands.UpdateMenu;

public class UpdateMenuCommand
    : IRequest<UpdateMenuResponse>
{
    public int MenuId { get; set; }

    public string Name { get; set; }
        = string.Empty;

    public string Description { get; set; }
        = string.Empty;

    public bool Status { get; set; }
}