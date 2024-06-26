
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Parking.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string ?Name { get; set; }
        [Required]
        [EmailAddress]
        public string ?Email { get; set; }
        [Required]
        [PasswordPropertyText]
        public string ?Password { get; set; }
        [Required]
        public string ?Phone  { get; set; }
        public string ?license { get; set; }

        public ICollection<Booking> ?Bookings { get; set; }

    }
}
