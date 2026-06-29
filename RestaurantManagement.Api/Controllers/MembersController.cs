using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Members.Commands.CreateMember;
using RestaurantManagement.Application.Members.Commands.ApplyPoints;
using RestaurantManagement.Application.Members.Queries;

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

        // UC_PAY_03: Lấy thông tin thành viên qua việc Quét thẻ (CardId)
        [HttpGet("card/{cardId}")]
        public async Task<IActionResult> GetMemberByCardId(string cardId)
        {
            var query = new GetMemberByCardIdQuery(cardId);
            var result = await _mediator.Send(query);
            
            if (result == null)
            {
                return NotFound(new { message = "Thẻ thành viên không tồn tại trong hệ thống." });
            }
            
            return Ok(result);
        }

        // UC_PAY_03: Thực hiện đổi điểm thưởng giảm giá trực tiếp
        [HttpPost("apply-points")]
        public async Task<IActionResult> ApplyPoints([FromBody] ApplyPointsCommand command)
        {
            if (command == null)
            {
                return BadRequest(new { message = "Dữ liệu yêu cầu không hợp lệ." });
            }

            try
            {
                var success = await _mediator.Send(command);
                return Ok(new { message = "Áp dụng điểm thưởng thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}