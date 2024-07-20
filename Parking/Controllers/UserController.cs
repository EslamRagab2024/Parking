using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Models;
using System.Security.Claims;

namespace Parking.Controllers
{
    public class UserController : Controller
    {
        private readonly MyDb db;

        public UserController(MyDb db)
        {
            this.db = db;
        }

        [HttpGet]

        public IActionResult Display()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var total=db.Bookings.Where(x=>x.Email == email).ToList(); 
            return View(total);
           
        }

        [HttpGet]
        public IActionResult Book()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Book(Booking booking)
        { 

            return View();
        }


    }
}
