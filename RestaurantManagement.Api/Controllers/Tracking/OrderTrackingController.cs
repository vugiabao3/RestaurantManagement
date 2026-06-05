using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.DTOs.Tracking;
using RestaurantManagement.Application.Interfaces.Tracking;

namespace RestaurantManagement.Api.Controllers.Tracking;

[ApiController]
[Route("api/tracking")]
public class OrderTrackingController : ControllerBase
{
    private readonly IOrderTrackingService _service;

    public OrderTrackingController(IOrderTrackingService service)
    {
        _service = service;
    }

    [HttpGet("progress/{tableId:int}")]
    public async Task<ActionResult<ApiResult<TienDoMonAnDto>>> LayTienDoMonAn(int tableId)
    {
        var data = await _service.LayTienDoMonAnAsync(tableId);
        return Ok(ApiResult<TienDoMonAnDto>.Ok(data, "Lấy tiến độ món ăn thành công."));
    }

    [HttpGet("notifications/{tableId:int}")]
    public async Task<ActionResult<ApiResult<IReadOnlyList<ThongBaoDto>>>> LayThongBao(int tableId)
    {
        var data = await _service.LayThongBaoAsync(tableId);
        return Ok(ApiResult<IReadOnlyList<ThongBaoDto>>.Ok(data, "Lấy thông báo thành công."));
    }

    [HttpPatch("notifications/{notificationId:int}/dismiss")]
    public async Task<ActionResult<ApiResult<object>>> DanhDauDaHienThi(int notificationId)
    {
        try
        {
            await _service.DanhDauDaHienThiThongBaoAsync(notificationId);
            return Ok(ApiResult<object>.Ok(new { notificationId }, "Đã đóng thông báo."));
        }
        catch (NghiepVuException ex)
        {
            return StatusCode(
                ex.StatusCode,
                ApiResult<object>.Fail(ex.Message)
            );
        }
    }
}
