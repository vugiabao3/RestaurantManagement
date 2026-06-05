using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Application.DTOs.Kitchen;
using RestaurantManagement.Application.Interfaces.Kitchen;

namespace RestaurantManagement.API.Controllers.Kitchen;

[ApiController]
[Route("api/kitchen")]
public class KitchenTrackingController : ControllerBase
{
    private readonly IKitchenTrackingService _service;
    private readonly IConfiguration _configuration;

    public KitchenTrackingController(IKitchenTrackingService service, IConfiguration configuration)
    {
        _service = service;
        _configuration = configuration;
    }

    [HttpGet("items")]
    public async Task<ActionResult<ApiResult<IReadOnlyList<MonAnBepDto>>>> LayDanhSachMonChoBep([FromQuery] string? status)
    {
        var data = await _service.LayDanhSachMonChoBepAsync(status);
        return Ok(ApiResult<IReadOnlyList<MonAnBepDto>>.Ok(data, "Lấy danh sách món cho bếp thành công."));
    }

    [HttpPatch("items/{orderItemId:int}/start-cooking")]
    public async Task<ActionResult<ApiResult<object>>> ChuyenSangDangCheBien(int orderItemId)
    {
        var user = LayNguoiDungHienTai();
        await _service.ChuyenSangDangCheBienAsync(orderItemId, user.UserId, user.Role);
        return Ok(ApiResult<object>.Ok(new { orderItemId }, "Món ăn đã chuyển sang trạng thái đang chế biến."));
    }

    [HttpPatch("items/{orderItemId:int}/complete")]
    public async Task<ActionResult<ApiResult<object>>> HoanThanhMon(int orderItemId)
    {
        var user = LayNguoiDungHienTai();
        await _service.HoanThanhMonAsync(orderItemId, user.UserId, user.Role);
        return Ok(ApiResult<object>.Ok(new { orderItemId }, "Cập nhật món hoàn thành thành công."));
    }

    [HttpPatch("items/{orderItemId:int}/cancel")]
    public async Task<ActionResult<ApiResult<object>>> HuyMon(int orderItemId)
    {
        var user = LayNguoiDungHienTai();
        await _service.HuyMonAsync(orderItemId, user.UserId, user.Role);
        return Ok(ApiResult<object>.Ok(new { orderItemId }, "Hủy món thành công."));
    }

    [HttpPost("items/{orderItemId:int}/delay")]
    public async Task<ActionResult<ApiResult<object>>> DanhDauChamMon(int orderItemId, [FromBody] DanhDauChamMonRequest request)
    {
        var user = LayNguoiDungHienTai();
        await _service.DanhDauChamMonAsync(orderItemId, request, user.UserId, user.Role);
        return Ok(ApiResult<object>.Ok(new { orderItemId }, "Đánh dấu món chậm thành công."));
    }

    [HttpDelete("items/{orderItemId:int}/delay")]
    public async Task<ActionResult<ApiResult<object>>> HuyDanhDauChamMon(int orderItemId, [FromBody] HuyDanhDauChamMonRequest request)
    {
        var user = LayNguoiDungHienTai();
        await _service.HuyDanhDauChamMonAsync(orderItemId, request, user.UserId, user.Role);
        return Ok(ApiResult<object>.Ok(new { orderItemId }, "Đã hủy trạng thái chậm món."));
    }

    [HttpPost("delays/auto-detect")]
    public async Task<ActionResult<ApiResult<AutoDetectDelayResult>>> TuDongPhatHienMonCham([FromBody] AutoDetectDelayRequest request)
    {
        var multiplier = request.ThresholdMultiplier ?? _configuration.GetValue<decimal>("DelayAutoDetect:ThresholdMultiplier", 1.5m);
        var systemUserId = _configuration.GetValue<int>("DelayAutoDetect:SystemUserId", 1);
        var result = await _service.TuDongPhatHienMonChamAsync(multiplier, systemUserId);
        return Ok(ApiResult<AutoDetectDelayResult>.Ok(result, "Tự động phát hiện món chậm thành công."));
    }

    private (int UserId, string Role) LayNguoiDungHienTai()
    {
        var userIdHeader = Request.Headers["x-user-id"].FirstOrDefault();
        var roleHeader = Request.Headers["x-user-role"].FirstOrDefault();

        var userId = int.TryParse(userIdHeader, out var parsedUserId) ? parsedUserId : 1;
        var role = string.IsNullOrWhiteSpace(roleHeader) ? "Đầu bếp" : roleHeader;

        return (userId, role);
    }
}
