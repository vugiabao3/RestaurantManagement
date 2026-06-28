using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application
    .Dishes.Queries.GetAllDishes;

public class GetAllDishesHandler
    : IRequestHandler<
        GetAllDishesQuery,
        List<GetAllDishesResponse>>
{
    private readonly IDishRepository _dishRepository;

    public GetAllDishesHandler(
        IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<List<GetAllDishesResponse>>
        Handle(
            GetAllDishesQuery request,
            CancellationToken cancellationToken)
    {
        var dishes =
            await _dishRepository.GetAllAsync();

        return dishes
            .Select(x =>
                new GetAllDishesResponse
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