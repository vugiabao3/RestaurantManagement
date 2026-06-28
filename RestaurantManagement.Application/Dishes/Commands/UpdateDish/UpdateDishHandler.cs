using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Dishes.Commands.UpdateDish;

public class UpdateDishHandler
    : IRequestHandler<
        UpdateDishCommand,
        UpdateDishResponse>
{
    private readonly IDishRepository _dishRepository;

    public UpdateDishHandler(
        IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<UpdateDishResponse> Handle(
        UpdateDishCommand request,
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

        dish.Name = request.Name;
        dish.Price = request.Price;
        dish.Description = request.Description;
        dish.Status = request.Status;
        dish.ImageUrl = request.ImageUrl;
        dish.CategoryId = request.CategoryId;

        await _dishRepository
            .UpdateAsync(dish);

        return new UpdateDishResponse
        {
            Message =
                "Dish updated successfully"
        };
    }
}