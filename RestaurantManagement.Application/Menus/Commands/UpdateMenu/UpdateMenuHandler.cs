using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application
    .Menus.Commands.UpdateMenu;

public class UpdateMenuHandler
    : IRequestHandler<
        UpdateMenuCommand,
        UpdateMenuResponse>
{
    private readonly IMenuRepository _menuRepository;

    public UpdateMenuHandler(
        IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }
    public async Task<UpdateMenuResponse> Handle(
    UpdateMenuCommand request,
    CancellationToken cancellationToken)
    {
        var menu =
            await _menuRepository
                .GetByIdAsync(request.MenuId);

        if (menu == null)
        {
            throw new Exception(
                "Menu not found");
        }

        menu.Name = request.Name;
        menu.Description = request.Description;
        menu.Status = request.Status;
        menu.UpdatedAt = DateTime.UtcNow;

        await _menuRepository.UpdateAsync(menu);

        return new UpdateMenuResponse
        {
            Message =
                "Menu updated successfully"
        };
    }
}