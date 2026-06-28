using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Orders.Commands.AcceptOrder;
using RestaurantManagement.Application.Orders.Commands.CancelOrder;
using RestaurantManagement.Application.Orders.Commands.CompleteOrder;
using RestaurantManagement.Application.Orders.Commands.NotifyOrderCreated;
using RestaurantManagement.Application.Orders.Commands.UndoOrderDetailStatus;
using RestaurantManagement.Application.Orders.Commands.UpdateOrderDetailStatus;
using RestaurantManagement.Application.Orders.Commands.UpdateOrderPriority;
using RestaurantManagement.Application.Orders.Queries.GetOrderStatusHistory;
using RestaurantManagement.Application.Orders.Queries.GetPendingOrders;
using RestaurantManagement.Domain.Constants;

namespace RestaurantManagement.API.Controllers
{
    [ApiController]
    [Route("api/kitchen/orders")]
    [Authorize]
    public class KitchenOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public KitchenOrdersController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Kitchen)]
        public async Task<IActionResult> GetPendingOrders([FromQuery] int? areaId) =>
            Ok(await _mediator.Send(new GetPendingOrdersQuery { KitchenAreaId = areaId }));

        [HttpPut("order-details/{id}/status")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Kitchen)]
        public async Task<IActionResult> UpdateOrderDetailStatus(int id, UpdateOrderDetailStatusCommand command)
        {
            command.OrderDetailId = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("order-details/{id}/undo")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Kitchen)]
        public async Task<IActionResult> UndoOrderDetailStatus(int id, UndoOrderDetailStatusCommand command)
        {
            command.OrderDetailId = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("{id}/notify-created")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Staff)]
        public async Task<IActionResult> NotifyOrderCreated(int id) =>
            Ok(await _mediator.Send(new NotifyOrderCreatedCommand { OrderId = id }));

        [HttpPut("{id}/accept")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Kitchen)]
        public async Task<IActionResult> AcceptOrder(int id) =>
            Ok(await _mediator.Send(new AcceptOrderCommand { OrderId = id }));

        [HttpPut("{id}/complete")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Kitchen)]
        public async Task<IActionResult> CompleteOrder(int id) =>
            Ok(await _mediator.Send(new CompleteOrderCommand { OrderId = id }));

        [HttpPut("{id}/cancel")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Kitchen)]
        public async Task<IActionResult> CancelOrder(int id, CancelOrderCommand command)
        {
            command.OrderId = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}/priority")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Staff)]
        public async Task<IActionResult> UpdatePriority(int id, UpdateOrderPriorityCommand command)
        {
            command.OrderId = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("{id}/status-history")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Kitchen)]
        public async Task<IActionResult> GetStatusHistory(int id) =>
            Ok(await _mediator.Send(new GetOrderStatusHistoryQuery { OrderId = id }));
    }
}
