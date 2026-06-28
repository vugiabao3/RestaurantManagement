using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.AuditLogs.Queries.SearchAuditLogs;
using RestaurantManagement.Domain.Constants;

namespace RestaurantManagement.API.Controllers
{
    [ApiController]
    [Route("api/audit-logs")]
    [Authorize(Roles = UserRoles.Admin)]
    public class AuditLogsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuditLogsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] DateTime? from, [FromQuery] DateTime? to,
            [FromQuery] int? userId, [FromQuery] string? action, [FromQuery] int take = 200) =>
            Ok(await _mediator.Send(new SearchAuditLogsQuery { From = from, To = to, UserId = userId, Action = action, Take = take }));
    }
}
