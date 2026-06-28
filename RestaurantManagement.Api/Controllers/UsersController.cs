using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Auth.Commands.SetUserRole;

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
}