using System;
using System.ComponentModel.DataAnnotations;

namespace HotDeskBookingSystem.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartAt { get; set; }

        [Required]
        public DateTime FinishAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public int DaysDuration { get; set; }


        public int DeskId { get; set; }
        public virtual Desk Desk { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}

