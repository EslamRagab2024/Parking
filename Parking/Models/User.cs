
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Parking.Models
{
    public class User
    {
        
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Key]
        public string? Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string? Password { get; set; }

       
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "License is required")]
        public string? license { get; set; }

        public ICollection<Booking>? Bookings { get; set; }

    }
}
