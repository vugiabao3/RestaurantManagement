using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application
    .Dishes.Queries.SearchDishes;

public class SearchDishesHandler
    : IRequestHandler<
        SearchDishesQuery,
        List<SearchDishesResponse>>
{
    private readonly IDishRepository _dishRepository;

    public SearchDishesHandler(
        IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<List<SearchDishesResponse>>
        Handle(
            SearchDishesQuery request,
            CancellationToken cancellationToken)
    {
        var dishes =
            await _dishRepository
                .SearchAsync(request.Keyword);

        return dishes
            .Select(x =>
                new SearchDishesResponse
                {
                    DishId = x.DishId,
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    Status = x.Status,
                    ImageUrl = x.ImageUrl,
                    CategoryId = x.CategoryId
                })
            .ToList();
    }
}