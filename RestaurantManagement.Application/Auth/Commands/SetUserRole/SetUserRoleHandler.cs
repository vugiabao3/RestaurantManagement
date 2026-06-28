using MediatR;
using RestaurantManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Auth.Commands.SetUserRole
{
    public class SetUserRoleHandler
    : IRequestHandler<
        SetUserRoleCommand,
        SetUserRoleResponse>
    {
        private readonly IUserRepository _userRepository;

        public SetUserRoleHandler(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<SetUserRoleResponse> Handle(
            SetUserRoleCommand request,
            CancellationToken cancellationToken)
        {
            var user =
                await _userRepository
                    .GetByIdAsync(request.UserId);

            if (user == null)
            {
                throw new Exception(
                    "User not found");
            }

            var validRoles = new[]
            {
            "Admin",
            "Kitchen",
            "Staff"
        };

            if (!validRoles.Contains(request.Role))
            {
                throw new Exception(
                    "Invalid role");
            }

            user.Role = request.Role;

            await _userRepository
                .UpdateAsync(user);

            return new SetUserRoleResponse
            {
                Message =
                    $"User role updated to {request.Role}"
            };
        }
    }
}

