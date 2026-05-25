using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Reports.Queries.GetRevenueReport;

namespace RestaurantManagement.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("revenue")]
        public async Task<IActionResult>
            GetRevenueReport(
                [FromQuery] DateTime startDate,
                [FromQuery] DateTime endDate)
        {
            var query = new GetRevenueReportQuery
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}