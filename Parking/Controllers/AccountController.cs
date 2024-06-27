using Microsoft.AspNetCore.Mvc;
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


        [HttpGet]
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

                return RedirectToAction("login");
            }

            return View(user);
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users
                    .SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    // Login success logic
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

    }
}
