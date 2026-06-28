using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Auth.Commands.ForgotPassword;

public class ForgotPasswordResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = null!;
}