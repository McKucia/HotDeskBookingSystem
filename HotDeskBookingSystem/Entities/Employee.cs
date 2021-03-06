using System;
using System.ComponentModel.DataAnnotations;

namespace HotDeskBookingSystem.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(25), MinLength(5)]
        public string FirstName { get; set; }

        [Required, MaxLength(25), MinLength(5)]
        public string SecondName { get; set; }

        public string PasswordHash { get; set; }


        public virtual List<Reservation> Reservations { get; set; }

        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}

