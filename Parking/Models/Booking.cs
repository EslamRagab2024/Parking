using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.Models
{
    public class Booking
    {
        [Key]
        public int ID { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
