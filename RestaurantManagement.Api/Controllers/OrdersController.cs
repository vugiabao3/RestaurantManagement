using MediatR;
using Microsoft.AspNetCore.Mvc;
// Sử dụng chính xác namespace Query cũ của bạn
using RestaurantManagement.Application.Payments.Queries.GetOrderForPayment; 

namespace RestaurantManagement.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// UC_PAY_01: Tra cứu hóa đơn chưa thanh toán theo mã bàn
        /// </summary>
        [HttpGet("unpaid/table/{tableNumber}")]
        public async Task<IActionResult> GetUnpaidOrderByTableId(string tableNumber, CancellationToken cancellationToken)
        {
            // Gọi chính xác Query đang có sẵn của bạn
            var query = new GetOrderForPaymentQuery(tableNumber);
            
            try
            {
                var result = await _mediator.Send(query, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Trả về lỗi MS01 như Use Case yêu cầu nếu không tìm thấy bàn/hóa đơn
                return NotFound(new { message = ex.Message });
            }
        }
    }
}