using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotDeskBookingSystem.Entities
{
    public enum Status
    {
        Available, Unavailable
    }

    public class Desk
    {
        public int Id { get; set; }
        [Required]
        public Status Status { get; set; } = Status.Available;


        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}

