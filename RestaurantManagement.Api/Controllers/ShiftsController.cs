using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Shifts.Commands.OpenShift;
using RestaurantManagement.Application.Shifts.Commands.CloseShift;
using RestaurantManagement.Application.Shifts.Queries.GetShiftReport;

namespace RestaurantManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShiftsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Mở ca làm việc mới
    /// </summary>
    [HttpPost("open")]
    public async Task<IActionResult> OpenShift([FromBody] OpenShiftCommand command)
    {
        var shiftId = await _mediator.Send(command);
        return Ok(new { Message = "Mở ca thành công", ShiftId = shiftId });
    }

    /// <summary>
    /// Chốt ca làm việc
    /// </summary>
    [HttpPut("close")]
    public async Task<IActionResult> CloseShift([FromBody] CloseShiftCommand command)
    {
        var result = await _mediator.Send(command);
        
        if (result)
        {
            return Ok(new { Message = "Chốt ca thành công" });
        }
        
        return BadRequest(new { Message = "Không thể chốt ca. Vui lòng kiểm tra lại." });
    }

    /// <summary>
    /// Lấy báo cáo tổng kết của một ca làm việc
    /// </summary>
    [HttpGet("{id}/report")]
    public async Task<IActionResult> GetShiftReport(int id)
    {
        var query = new GetShiftReportQuery(id);
        var report = await _mediator.Send(query);
        
        return Ok(report);
    }
}