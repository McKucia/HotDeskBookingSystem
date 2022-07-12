using System;
using System.ComponentModel.DataAnnotations;

namespace HotDeskBookingSystem.Entities
{
    public class LoginEmployeeDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}

