using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.MenuItems.Commands.CreateMenuItem;
using RestaurantManagement.Application.MenuItems.Commands.DeleteMenuItem;
using RestaurantManagement.Application.MenuItems.Commands.ToggleMenuItemAvailability;
using RestaurantManagement.Application.MenuItems.Commands.UpdateMenuItem;
using RestaurantManagement.Application.MenuItems.Queries.GetMenuCategories;
using RestaurantManagement.Application.MenuItems.Queries.GetMenuItemsByCategory;
using RestaurantManagement.Domain.Constants;

namespace RestaurantManagement.API.Controllers
{
    [ApiController]
    [Route("api/menu-items")]
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Kitchen)]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MenuItemsController(IMediator mediator) => _mediator = mediator;

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories() => Ok(await _mediator.Send(new GetMenuCategoriesQuery()));

        [HttpGet]
        public async Task<IActionResult> GetByCategory([FromQuery] string? category) =>
            Ok(await _mediator.Send(new GetMenuItemsByCategoryQuery { Category = category }));

        [HttpPost]
        public async Task<IActionResult> Create(CreateMenuItemCommand command) => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMenuItemCommand command)
        {
            command.MenuItemId = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _mediator.Send(new DeleteMenuItemCommand { MenuItemId = id }));

        [HttpPatch("{id}/availability")]
        public async Task<IActionResult> ToggleAvailability(int id, ToggleMenuItemAvailabilityCommand command)
        {
            command.MenuItemId = id;
            return Ok(await _mediator.Send(command));
        }
    }
}
