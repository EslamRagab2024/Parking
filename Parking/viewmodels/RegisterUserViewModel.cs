
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Parking.viewmodels
{
    [Keyless]
    public class RegisterUserViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        public string ?Email { get; set; }

        [Required]
        [Display(Name = "user name")]
        public string ?UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "password")]
        public string ?Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        [Display(Name = "confirm password")]
        public string ?ConfirmPassword { get; set; }
    }
}
