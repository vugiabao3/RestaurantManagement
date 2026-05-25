using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Auth.Commands.Login
{
    public class LoginHandler
        : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IAdminRepository _adminRepository;

        private readonly IJwtTokenGenerator _jwt;

        public LoginHandler(
            IAdminRepository adminRepository,
            IJwtTokenGenerator jwt)
        {
            _adminRepository = adminRepository;
            _jwt = jwt;
        }

        public async Task<LoginResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var admin = await _adminRepository
                .GetByUsernameAsync(request.Username);

            if (admin == null)
            {
                throw new Exception("Username not found");
            }

            if (admin.Password != request.Password)
            {
                throw new Exception("Wrong password");
            }

            var token = _jwt.GenerateToken(admin);

            return new LoginResponse
            {
                Token = token
            };
        }
    }
}
