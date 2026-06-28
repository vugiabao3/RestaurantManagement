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
        private readonly IUserRepository _userRepository;

        private readonly IJwtTokenGenerator _jwt;

        public LoginHandler(
            IUserRepository userRepository,
            IJwtTokenGenerator jwt)
        {
            _userRepository = userRepository;
            _jwt = jwt;
        }

        public async Task<LoginResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (user.Password != request.Password)
            {
                throw new Exception("Wrong password");
            }

            var token = _jwt.GenerateToken(user);

            return new LoginResponse
            {
                Token = token,
                Role = user.Role

            };
        }
    }
}
