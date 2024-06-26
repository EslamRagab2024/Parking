using Microsoft.AspNetCore.Mvc;
using Parking.Models;

namespace Parking.Controllers
{
    public class AccountController : Controller
    {
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Register(User user)
        {
            
            return View();
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Login( [FromBody]String Email ,[FromBody]String password)
        {
            return View();
        }

    }
}
