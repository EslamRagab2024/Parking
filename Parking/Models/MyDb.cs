using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parking.viewmodels;


namespace Parking.Models
{
    public class MyDb:IdentityDbContext<ApplicationUser>
    {
        public MyDb(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<Booking> ?Bookings { get; set; }
        
        public DbSet<Parking.viewmodels.RegisterUserViewModel>? RegisterUserViewModel { get; set; }
        
        public DbSet<Parking.viewmodels.LoginViewModel>? LoginViewModel { get; set; }
        
        public DbSet<Parking.viewmodels.Roleview>? Roleview { get; set; }
       

    }
}
