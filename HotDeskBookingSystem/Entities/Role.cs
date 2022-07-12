using System.ComponentModel.DataAnnotations;

namespace HotDeskBookingSystem.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

