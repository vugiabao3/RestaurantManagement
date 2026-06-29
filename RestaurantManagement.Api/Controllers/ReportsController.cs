using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Reports.Queries.GetRevenueReport;

namespace RestaurantManagement.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    // Bỏ [Authorize(Roles = "Admin")] ở mức Controller để cấu hình riêng cho từng API
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 1. API cũ của Admin (Tìm theo ngày)
        [HttpGet("revenue")]
        [Authorize(Roles = "Admin")] // Chỉ Admin dùng được
        public async Task<IActionResult> GetRevenueReport(
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

        // 2. API mới của Thu Ngân (Tìm theo tháng)
        [HttpGet("monthly-revenue")]
        [Authorize(Roles = "Admin,Cashier")] // Cả Admin và Thu ngân đều dùng được
        public async Task<IActionResult> GetMonthlyRevenueReport(
            [FromQuery] int month, 
            [FromQuery] int year)
        {
            if (month < 1 || month > 12 || year < 2000) return BadRequest("Tháng/Năm không hợp lệ");

            // Dùng tên class mới
            var query = new GetMonthlyRevenueReportQuery(month, year);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}