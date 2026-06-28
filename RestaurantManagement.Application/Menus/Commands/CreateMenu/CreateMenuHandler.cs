using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application
    .Menus.Commands.CreateMenu;

public class CreateMenuHandler
    : IRequestHandler<
        CreateMenuCommand,
        CreateMenuResponse>
{
    private readonly IMenuRepository _menuRepository;

    public CreateMenuHandler(
        IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<CreateMenuResponse> Handle(
        CreateMenuCommand request,
        CancellationToken cancellationToken)
    {
        var menu = new Menu
        {
            Name = request.Name,
            Description = request.Description,
            Status = request.Status,

            CreatedAt = DateTime.UtcNow
        };

        await _menuRepository.AddAsync(menu);

        return new CreateMenuResponse
        {
            Message =
                "Menu created successfully"
        };
    }
}