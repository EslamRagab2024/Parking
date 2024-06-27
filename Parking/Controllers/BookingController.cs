using Microsoft.AspNetCore.Mvc;
using Parking.Models;

namespace Parking.Controllers
{
    public class BookingController : Controller
    {
        private  MyDb _context;

        public BookingController(MyDb context)
        {
            _context = context;
        }

        
        public IActionResult Book()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Book(Booking model)
        {
            if (ModelState.IsValid)
            {
                _context?.Bookings?.Add(model);
                _context?.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError("wrong book", "enter all fields");
                return View();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
