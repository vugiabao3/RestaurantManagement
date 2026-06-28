using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Analytics.Queries.GetKitchenAnalytics;
using RestaurantManagement.Domain.Constants;

namespace RestaurantManagement.API.Controllers
{
    [ApiController]
    [Route("api/kitchen/analytics")]
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Kitchen)]
    public class KitchenAnalyticsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public KitchenAnalyticsController(IMediator mediator) => _mediator = mediator;

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] int? areaId) =>
            Ok(await _mediator.Send(new GetKitchenAnalyticsQuery { From = from, To = to, KitchenAreaId = areaId }));
    }
}
