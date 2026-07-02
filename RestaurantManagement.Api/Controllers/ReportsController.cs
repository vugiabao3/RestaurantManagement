using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Reports.Queries.GetRevenueReport;
using RestaurantManagement.Application.Reports.Queries.RevenueByYear;

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

        /// <summary>
        /// Lấy báo cáo doanh thu theo năm
        /// </summary>
        /// <param name="year">Ví dụ: 2026</param>
        /// <returns>Danh sách doanh thu 12 tháng trong năm</returns>
        [HttpGet("revenue/{year}")]
        public async Task<IActionResult> GetRevenueByYear(int year)
        {
            var result = await _mediator.Send(
                new RevenueByYearQuery
                {
                    Year = year
                });

            return Ok(result);
        }
    }
}