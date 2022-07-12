using System;
using System.ComponentModel.DataAnnotations;

namespace HotDeskBookingSystem.Entities
{
    public class RegisterEmployeeDto
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(25), MinLength(5)]
        public string FirstName { get; set; }

        [Required, MaxLength(25), MinLength(5)]
        public string SecondName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public int RoleId { get; set; } = 2; // employee
    }
}

