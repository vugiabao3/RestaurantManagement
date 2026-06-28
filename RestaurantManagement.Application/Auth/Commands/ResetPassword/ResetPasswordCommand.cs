using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace RestaurantManagement.Application.Auth.Commands.ResetPassword;

public record ResetPasswordCommand(
    string Email,
    string Otp,
    string NewPassword
) : IRequest<ResetPasswordResponse>;
