using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? LastLogin { get; set; }

        public bool Status { get; set; }

        public string? ResetPasswordOtp { get; set; }

        public DateTime? ResetPasswordOtpExpiry { get; set; }
    }
}
