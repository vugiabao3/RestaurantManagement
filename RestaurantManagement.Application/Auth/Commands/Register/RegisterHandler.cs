using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;


namespace RestaurantManagement.Application.Auth.Commands.Register
{
    public class RegisterHandler
        : IRequestHandler<
            RegisterCommand,
            RegisterResponse>
    {
        private readonly IUserRepository _userRepository;

        public RegisterHandler(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegisterResponse> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _userRepository
                .UsernameExistsAsync(request.Username);

            if (exists)
            {
                throw new Exception(
                    "Username already exists");
            }

            var user = new User
            {
                Username = request.Username,
                Password = request.Password,
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone,
                Role="User",
                CreatedAt = DateTime.Now,
                Status = true
            };

            await _userRepository.AddAsync(user);

            return new RegisterResponse
            {
                Message = "Register successful"
            };
        }
    }
}
