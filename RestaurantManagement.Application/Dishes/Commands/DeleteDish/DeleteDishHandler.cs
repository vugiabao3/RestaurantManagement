using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Dishes.Commands.DeleteDish;

public class DeleteDishHandler
    : IRequestHandler<
        DeleteDishCommand,
        DeleteDishResponse>
{
    private readonly IDishRepository _dishRepository;

    public DeleteDishHandler(
        IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<DeleteDishResponse> Handle(
        DeleteDishCommand request,
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

        await _dishRepository
            .DeleteAsync(dish);

        return new DeleteDishResponse
        {
            Message =
                "Dish deleted successfully"
        };
    }
}