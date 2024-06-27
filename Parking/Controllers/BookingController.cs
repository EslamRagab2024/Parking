using Microsoft.AspNetCore.Mvc;
using Parking.Models;

namespace Parking.Controllers
{
    public class BookingController : Controller
    {
        private readonly MyDb _context;

        public BookingController(MyDb context)
        {
            _context = context;
        }

        // GET: Booking/BookParking
        public IActionResult BookParking()
        {
            return View();
        }

        // POST: Booking/BookParking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookParking(Booking model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.Email == User.Identity.Name);
                if (user != null)
                {
                    model.UserId = user.ID;
                    _context.Bookings.Add(model);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
