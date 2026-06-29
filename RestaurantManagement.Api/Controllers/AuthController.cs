using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Auth.Commands.ForgotPassword;
using RestaurantManagement.Application.Auth.Commands.Login;
using RestaurantManagement.Application.Auth.Commands.Register;
using RestaurantManagement.Application.Auth.Commands.ResetPassword;


namespace RestaurantManagement.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(
           RegisterCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(
    ForgotPasswordCommand command)
        {
            var result =
                await _mediator.Send(command);

            return Ok(result);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(
    ResetPasswordCommand command)
        {
            var result =
                await _mediator.Send(command);

            return Ok(result);
        }
    }
}