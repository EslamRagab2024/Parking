using System.ComponentModel.DataAnnotations;

namespace Parking.Models
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
       
        public string ?Name { get; set; }
       
        public string ?Email { get; set; }
        
        public string ?Password { get; set; }
    }
}
