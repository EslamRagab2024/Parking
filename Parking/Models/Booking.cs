using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.Models
{
    public class Booking
    {
        
        public int ID { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Key]
        public string? Email { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        public bool IsConfirmed { get; set; }
        // Foreign key to User
        [ForeignKey("Email")]
        public User? User { get; set; }
    }
}
