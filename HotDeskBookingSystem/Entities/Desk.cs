using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;

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
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; } = Status.Available;


        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}

