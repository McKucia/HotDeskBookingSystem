using System;
using System.ComponentModel.DataAnnotations;

namespace HotDeskBookingSystem.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, MaxLength(25), MinLength(5)]
        public string FirstName { get; set; }

        [Required, MaxLength(25), MinLength(5)]
        public string SecondName { get; set; }


        public virtual List<Reservation> Reservations { get; set; }
    }
}

