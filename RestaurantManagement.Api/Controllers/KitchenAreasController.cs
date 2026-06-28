using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.KitchenAreas.Commands.CreateKitchenArea;
using RestaurantManagement.Application.KitchenAreas.Commands.DeleteKitchenArea;
using RestaurantManagement.Application.KitchenAreas.Commands.UpdateKitchenArea;
using RestaurantManagement.Application.KitchenAreas.Queries.GetKitchenAreas;
using RestaurantManagement.Domain.Constants;

namespace RestaurantManagement.API.Controllers
{
    [ApiController]
    [Route("api/kitchen-areas")]
    [Authorize]
    public class KitchenAreasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public KitchenAreasController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Kitchen)]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetKitchenAreasQuery()));

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Create(CreateKitchenAreaCommand command) => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Update(int id, UpdateKitchenAreaCommand command)
        {
            command.KitchenAreaId = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(new { message = await _mediator.Send(new DeleteKitchenAreaCommand { KitchenAreaId = id }) });
    }
}
