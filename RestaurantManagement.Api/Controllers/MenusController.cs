using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application
    .Menus.Commands.CreateMenu;
using RestaurantManagement.Application
    .Menus.Commands.DeleteMenu;
using RestaurantManagement.Application
    .Menus.Commands.UpdateMenu;
using RestaurantManagement.Application.Menus.Queries.GetAllMenus;
using RestaurantManagement.Application.Menus.Queries.GetCategoriesByMenu;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/menus")]
public class MenusController
    : ControllerBase
{
    private readonly IMediator _mediator;

    public MenusController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(
        CreateMenuCommand command)
    {
        return Ok(
            await _mediator.Send(command));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(
        int id,
        UpdateMenuCommand command)
    {
        command.MenuId = id;

        return Ok(
            await _mediator.Send(command));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(
        int id)
    {
        return Ok(
            await _mediator.Send(
                new DeleteMenuCommand
                {
                    MenuId = id
                }));
    }

    [HttpGet("{id}/categories")]
    public async Task<IActionResult>
GetCategoriesByMenu(int id)
    {
        var result =
            await _mediator.Send(
                new GetCategoriesByMenuQuery(id));

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var menus = await _mediator.Send(new GetAllMenusQuery());
        return Ok(menus);
    }

}