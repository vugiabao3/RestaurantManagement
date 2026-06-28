using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application
    .Dishes.Queries.GetDishById;

public class GetDishByIdHandler
    : IRequestHandler<
        GetDishByIdQuery,
        GetDishByIdResponse>
{
    private readonly IDishRepository _dishRepository;

    public GetDishByIdHandler(
        IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<GetDishByIdResponse>
        Handle(
            GetDishByIdQuery request,
            CancellationToken cancellationToken)
    {
        var dish =
            await _dishRepository
                .GetByIdAsync(request.DishId);

        if (dish == null)
        {
            throw new Exception(
                "Dish not found");
        }

        return new GetDishByIdResponse
        {
            DishId = dish.DishId,
            Name = dish.Name,
            Price = dish.Price,
            Description = dish.Description,
            Status = dish.Status,
            CategoryId = dish.CategoryId
        };
    }
}