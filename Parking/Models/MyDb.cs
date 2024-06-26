using Microsoft.EntityFrameworkCore;
using System;

namespace Parking.Models
{
    public class MyDb:DbContext
    {
        public MyDb(DbContextOptions<MyDb> options) : base(options)
        {

        }
        public DbSet<User> ?Users { get; set; }
        public DbSet<Worker> ?Workers { get; set; }
        public DbSet<Admin> ?Admins { get; set; }
        public DbSet<Booking> ?Bookings { get; set; }

    }
}
