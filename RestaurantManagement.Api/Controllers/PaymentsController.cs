using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Payments.Commands.ProcessPayment;
using RestaurantManagement.Application.Payments.Queries.GetOrderForPayment;

namespace RestaurantManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy thông tin hóa đơn tạm tính để hiển thị lên màn hình thu ngân
    /// Khớp với thao tác: Thu ngân bấm vào "Bàn #05" đang có màu cam trên UI
    /// </summary>
    /// <param name="orderId">Mã đơn hàng</param>
    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetOrderSummary(int orderId)
    {
        try
        {
            var query = new GetOrderForPaymentQuery(orderId);
            
            // MediatR sẽ tự động tìm GetOrderForPaymentHandler để xử lý
            var result = await _mediator.Send(query); 
            
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Trả về lỗi 400 kèm message nếu đơn hàng không tồn tại hoặc đã thanh toán
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Thực hiện chốt thanh toán và lưu hóa đơn
    /// Khớp với thao tác: Thu ngân bấm nút "Tiền mặt" hoặc "QR Code" trên UI
    /// </summary>
    /// <param name="command">Payload chứa OrderId, CashierId, PaymentMethod...</param>
    [HttpPost("process")]
    public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentCommand command)
    {
        try
        {
            // MediatR sẽ tự động tìm ProcessPaymentHandler để thực thi logic Database
            var invoiceId = await _mediator.Send(command);
            
            return Ok(new 
            { 
                message = "Thanh toán thành công!", 
                invoiceId = invoiceId 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}