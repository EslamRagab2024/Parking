using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Parking.Models
{
    public class Worker
    {
        public int ID { get; set; }
        [Required]
        public string ?Name { get; set; }
        [Required]
        [EmailAddress]
        public string ?Email { get; set; }
        [PasswordPropertyText]
        [Required]
        public string ?Password { get; set; }

    }
}
