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
        private readonly IAdminRepository _adminRepository;

        public RegisterHandler(
            IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<RegisterResponse> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _adminRepository
                .UsernameExistsAsync(request.Username);

            if (exists)
            {
                throw new Exception(
                    "Username already exists");
            }

            var admin = new Admin
            {
                Username = request.Username,
                Password = request.Password,
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone,
                CreatedAt = DateTime.Now,
                Status = true
            };

            await _adminRepository.AddAsync(admin);

            return new RegisterResponse
            {
                Message = "Register successful"
            };
        }
    }
}
