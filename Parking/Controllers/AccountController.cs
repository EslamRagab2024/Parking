using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Models;

namespace Parking.Controllers
{
    public class AccountController : Controller
    {
        private  MyDb _context;

        public AccountController(MyDb context)
        {
            _context = context;
        }


        
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Register(User user)
        {

            if (ModelState.IsValid)
            {
                _context?.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(user);
        }



        
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _context.Users.SingleOrDefaultAsync
                (u =>u.Email==model.Email&&u.Password==model.Password);
            if (user != null)
            {
                return RedirectToAction("Book", "Booking");
            }
            var worker = await _context.Workers.SingleOrDefaultAsync
                (w => w.Email == model.Email && w.Password == model.Password);
            if (worker != null)
            {
                return RedirectToAction("Index", "Worker");
            }           
            return RedirectToAction("Login","Account");
        }
    }

}

