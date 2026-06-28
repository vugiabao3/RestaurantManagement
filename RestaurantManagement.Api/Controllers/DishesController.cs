using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Dishes.Commands.CreateDish;
using RestaurantManagement.Application.Dishes.Commands.DeleteDish;
using RestaurantManagement.Application.Dishes.Commands.UpdateDish;
using RestaurantManagement.Application.Dishes.Queries.GetAllDishes;
using RestaurantManagement.Application.Dishes.Queries.GetDishById;
using RestaurantManagement.Application.Dishes.Queries.SearchDishes;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/dishes")]
public class DishesController
    : ControllerBase
{
    private readonly IMediator _mediator;

    public DishesController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Chef")]
    public async Task<IActionResult> Create(
        CreateDishCommand command)
    {
        var result =
            await _mediator.Send(command);

        return Ok(result);
    }
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Chef")]
    public async Task<IActionResult> Update(
    int id,
    UpdateDishCommand command)
    {
        command.DishId = id;

        var result =
            await _mediator.Send(command);

        return Ok(result);
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(
    int id)
    {
        var command =
            new DeleteDishCommand
            {
                DishId = id
            };

        var result =
            await _mediator.Send(command);

        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result =
            await _mediator.Send(
                new GetAllDishesQuery());

        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(
    int id)
    {
        var query =
            new GetDishByIdQuery
            {
                DishId = id
            };

        var result =
            await _mediator.Send(query);

        return Ok(result);
    }
    [HttpGet("search")]
    public async Task<IActionResult> Search(
    [FromQuery] string keyword)
    {
        var query =
            new SearchDishesQuery
            {
                Keyword = keyword
            };

        var result =
            await _mediator.Send(query);

        return Ok(result);
    }


}