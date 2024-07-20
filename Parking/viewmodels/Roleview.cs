using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Parking.viewmodels
{
    [Keyless]
    public class Roleview
    {
        [Required]
        public string ?Name { get; set; }
    }
}
