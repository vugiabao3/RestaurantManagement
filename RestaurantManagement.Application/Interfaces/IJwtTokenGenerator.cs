using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Admin admin);
    }
}