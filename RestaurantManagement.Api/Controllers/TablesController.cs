using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Tables.Queries.GetAllTables;
using RestaurantManagement.Application.Tables.Queries.GetAvailableTables;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TablesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TablesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy danh sách tất cả bàn
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllTables()
    {
        var result = await _mediator.Send(new GetAllTablesQuery());

        return Ok(result);
    }
    /// <summary>
    /// Lấy danh sách bàn còn trống
    /// </summary>
    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableTables()
    {
        var result =
            await _mediator.Send(
                new GetAvailableTablesQuery());

        return Ok(result);
    }
}