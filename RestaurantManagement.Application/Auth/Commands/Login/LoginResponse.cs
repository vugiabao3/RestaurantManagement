using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RestaurantManagement.Application.Auth.Commands.Login
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Role{ get; set; } = string.Empty;
    }
}
