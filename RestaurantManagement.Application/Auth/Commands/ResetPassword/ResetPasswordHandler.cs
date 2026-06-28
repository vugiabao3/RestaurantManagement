using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Auth.Commands.ResetPassword;

public class ResetPasswordHandler
    : IRequestHandler<
        ResetPasswordCommand,
        ResetPasswordResponse>
{
    private readonly IUserRepository _userRepository;

    public ResetPasswordHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResetPasswordResponse> Handle(
        ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Tìm admin
        var user =
            await _userRepository
                .GetByEmailAsync(request.Email);

        if (user is null)
        {
            return new ResetPasswordResponse
            {
                Success = false,
                Message = "Email không tồn tại"
            };
        }

        // 2. Check OTP
        if (user.ResetPasswordOtp != request.Otp)
        {
            return new ResetPasswordResponse
            {
                Success = false,
                Message = "OTP không đúng"
            };
        }

        // 3. Check hết hạn
        if (user.ResetPasswordOtpExpiry < DateTime.UtcNow)
        {
            return new ResetPasswordResponse
            {
                Success = false,
                Message = "OTP đã hết hạn"
            };
        }

        // 4. Hash password mới
        user.Password =
            BCrypt.Net.BCrypt.HashPassword(
                request.NewPassword);

        // 5. Xóa OTP
        user.ResetPasswordOtp = null;

        user.ResetPasswordOtpExpiry = null;

        // 6. Save DB
        await _userRepository.UpdateAsync(user);

        return new ResetPasswordResponse
        {
            Success = true,
            Message = "Đổi mật khẩu thành công"
        };
    }
}
