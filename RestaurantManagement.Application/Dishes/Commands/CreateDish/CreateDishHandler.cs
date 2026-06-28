using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Dishes.Commands.CreateDish;

public class CreateDishHandler
    : IRequestHandler<
        CreateDishCommand,
        CreateDishResponse>
{
    private readonly IDishRepository _dishRepository;

    public CreateDishHandler(
        IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<CreateDishResponse> Handle(
        CreateDishCommand request,
        CancellationToken cancellationToken)
    {
        var dish = new Dish
        {
            Name = request.Name,
            Price = request.Price,
            Description = request.Description,
            Status = request.Status,
            ImageUrl = request.ImageUrl,
            CategoryId = request.CategoryId
        };

        await _dishRepository.AddAsync(dish);

        return new CreateDishResponse
        {
            Message = "Dish created successfully"
        };
    }
}