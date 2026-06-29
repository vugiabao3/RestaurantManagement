using MediatR;
using Microsoft.AspNetCore.Mvc;
// Thay thế bằng namespace chính xác chứa Command của bạn trong tầng Application
using RestaurantManagement.Application.Payments.Commands.ProcessPayment; 

namespace RestaurantManagement.API.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// UC_PAY_02: Xử lý thanh toán hóa đơn (Gộp hóa đơn, Đổi trạng thái bàn, Tích/Tiêu điểm)
        /// </summary>
        [HttpPost("checkout")]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
            {
                return BadRequest(new { message = "Dữ liệu yêu cầu thanh toán không hợp lệ." });
            }

            // Gửi lệnh xuống Application xử lý Database Transaction 
            // Đảm bảo tính toàn vẹn giữa CustomerOrder, Invoice, DiningTable và MemberCard
            var invoiceResult = await _mediator.Send(command, cancellationToken);
            
            // Trả về kết quả hóa đơn vừa được tạo thành công cùng mã HTTP 200 hoặc HTTP 201
            return Ok(invoiceResult);
        }
    }
}