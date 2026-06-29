using MediatR;
using Microsoft.AspNetCore.Mvc;
// Đã sửa lại đường dẫn trỏ đúng vào folder CreateMember của bạn
using RestaurantManagement.Application.Members.Commands.CreateMember;

namespace RestaurantManagement.API.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MembersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MembersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        // Đã đổi RegisterMemberCardCommand thành CreateMemberCommand
        public async Task<IActionResult> RegisterMember([FromBody] CreateMemberCommand command) 
        {
            if (command == null)
            {
                return BadRequest(new { message = "Dữ liệu đăng ký không hợp lệ." });
            }

            try
            {
                var memberId = await _mediator.Send(command);
                return Ok(new 
                { 
                    message = "Đăng ký thành viên thành công!", 
                    memberId = memberId 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}