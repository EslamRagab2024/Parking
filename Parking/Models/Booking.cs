using System.ComponentModel.DataAnnotations;

namespace Parking.Models
{
    public class Booking
    {
        
        public int ID { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        public string ?License { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        public bool IsConfirmed { get; set; }
        
    }
}
