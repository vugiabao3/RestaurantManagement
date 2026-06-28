using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Auth.Commands.ForgotPassword;

public class ForgotPasswordHandler
    : IRequestHandler<
        ForgotPasswordCommand,
        ForgotPasswordResponse>
{
    private readonly IUserRepository _userRepository;

    private readonly IEmailService _emailService;

    public ForgotPasswordHandler(
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _userRepository = userRepository;

        _emailService = emailService;
    }

    public async Task<ForgotPasswordResponse> Handle(
        ForgotPasswordCommand request,
        CancellationToken cancellationToken)
    {
        // 1. Tìm admin
        var user =
            await _userRepository
                .GetByEmailAsync(request.Email);

        if (user is null)
        {
            return new ForgotPasswordResponse
            {
                Success = false,
                Message = "Email không tồn tại"
            };
        }

        // 2. Tạo OTP 6 số
        var random = new Random();

        var otp =
            random.Next(100000, 999999).ToString();

        // 3. Lưu OTP
        user.ResetPasswordOtp = otp;

        user.ResetPasswordOtpExpiry =
            DateTime.UtcNow.AddMinutes(5);

        // 4. Update DB
        await _userRepository.UpdateAsync(user);

        // 5. Gửi email
        await _emailService.SendEmailAsync(
            user.Email,
            "Reset Password OTP",
            $"Mã OTP reset password của bạn là: {otp}"
        );

        return new ForgotPasswordResponse
        {
            Success = true,
            Message = "Đã gửi OTP về email"
        };
    }
}