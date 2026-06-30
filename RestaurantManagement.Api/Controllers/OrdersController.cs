using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Orders.Commands.AddDishToCart;
using RestaurantManagement.Application.Orders.Commands.ClearCart;
using RestaurantManagement.Application.Orders.Commands.PlaceOrder;
using RestaurantManagement.Application.Orders.Commands.RemoveOrderItem;
using RestaurantManagement.Application
.Orders.Commands.UpdateOrderItemQuantity;
using RestaurantManagement.Application.Orders.Commands.UpdateOrderStatus;
using RestaurantManagement.Application.Orders.Queries.GetCart;
using RestaurantManagement.Application.Orders.Queries.GetPendingOrders;
namespace RestaurantManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/orders/cart/items
        [HttpPost("cart/items")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddDish(AddDishToCartCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST: api/orders/place
        [HttpPost("place")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> PlaceOrder(PlaceOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("cart/items")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult>
UpdateQuantity(

UpdateOrderItemQuantityCommand command)
        {
            var result =
                await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("cart/items/{orderItemId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RemoveDish(
    int orderItemId)
        {
            var command =
                new RemoveOrderItemCommand
                {
                    OrderItemId = orderItemId
                };

            var result =
                await _mediator.Send(command);

            return Ok(result);
        }


        [HttpDelete("cart")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult>
ClearCart(
    [FromBody]
    ClearCartCommand command)
        {
            var result =
                await _mediator.Send(command);

            return Ok(result);
        }


        [HttpGet("pending")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> GetPendingOrders()
        {
            var result = await _mediator.Send(new GetPendingOrdersQuery());
            return Ok(result);
        }

        [HttpPut("status")]
        [Authorize(Roles = "Chef")]
        public async Task<IActionResult> UpdateStatus(
    UpdateOrderStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("cart/{customerId}")]
        public async Task<IActionResult> GetCart(int customerId)
        {
            var result = await _mediator.Send(new GetCartQuery
            {
                CustomerId = customerId
            });

            return Ok(result);
        }
    }
}