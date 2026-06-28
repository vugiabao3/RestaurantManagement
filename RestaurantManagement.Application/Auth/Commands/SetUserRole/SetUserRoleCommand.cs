using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Auth.Commands.SetUserRole
{
    public class SetUserRoleCommand
    : IRequest<SetUserRoleResponse>
    {
        public int UserId { get; set; }

        public string Role { get; set; } = string.Empty;
    }
}
