using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Members.Commands.RegisterMember;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/members")]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public MembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [Authorize(Roles = "Cashier")]
    public async Task<IActionResult> Register(
        RegisterMemberCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}