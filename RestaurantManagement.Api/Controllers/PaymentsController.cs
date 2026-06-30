using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Payments.Commands.CheckoutPayment;
using RestaurantManagement.Application.Payments.Queries.PreviewInvoice;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("preview/{orderId}")]
    [Authorize(Roles = "Cashier")]
    public async Task<IActionResult> PreviewInvoice(
        int orderId)
    {
        var result =
            await _mediator.Send(
                new PreviewInvoiceQuery
                {
                    OrderId = orderId
                });

        return Ok(result);
    }
    [HttpPost("checkout")]
    [Authorize(Roles = "Cashier")]
    public async Task<IActionResult> Checkout(
    CheckoutPaymentCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}