using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Auth.Commands.SetUserRole;
using RestaurantManagement.Application.Users.Queries.GetAllUsers;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("role")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SetRole(
        
        SetUserRoleCommand command)
    {

        var result =
            await _mediator.Send(command);

        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result =
            await _mediator.Send(
                new GetAllUsersQuery());

        return Ok(result);
    }
}