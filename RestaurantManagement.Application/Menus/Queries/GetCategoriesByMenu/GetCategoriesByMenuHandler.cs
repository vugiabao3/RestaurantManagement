using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;


namespace RestaurantManagement.Application
.Menus.Queries.GetCategoriesByMenu;

public class GetCategoriesByMenuHandler
    : IRequestHandler<
        GetCategoriesByMenuQuery,
        List<GetCategoriesByMenuResponse>>
{
    private readonly IMenuRepository _menuRepository;

    public GetCategoriesByMenuHandler(
        IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<List<GetCategoriesByMenuResponse>>
        Handle(
            GetCategoriesByMenuQuery request,
            CancellationToken cancellationToken)
    {
        var categories =
            await _menuRepository
                .GetCategoriesByMenuAsync(
                    request.MenuId);

        return categories.Select(c =>
            new GetCategoriesByMenuResponse
            {
                CategoryId = c.CategoryId,

                Name = c.Name,

                Description = c.Description,

                Status = c.Status,

                CreatedAt = c.CreatedAt,

                UpdatedAt = c.UpdatedAt,

                MenuId = c.MenuId
            })
            .ToList();
    }
}