using System;
using System.ComponentModel.DataAnnotations;

namespace HotDeskBookingSystem.Entities
{
    public class Location
    {
        public int Id { get; set; }

        [Required, Range(0, 3, ErrorMessage = "{0} must be greater than {1}")]
        public int FloorNumber { get; set; }

        [Required, Range(0, 5, ErrorMessage = "{0} must be greater than {1}")]
        public int RoomNumber { get; set; }


        public virtual Desk Desk { get; set; }
    }
}

