using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Parking.Models;
using Parking.viewmodels;
using System.Security.Claims;

namespace Parking.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public RolesController(RoleManager<IdentityRole>roleManager, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) 
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(Roleview roleview)
        {
            if(ModelState.IsValid)
            {
                IdentityRole role=new IdentityRole();
                role.Name = roleview.Name;
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return View(new Roleview());
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(roleview);
        }
        [HttpGet]
        public IActionResult addworker()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> addworker(RegisterUserViewModel register)
        {
            if (ModelState.IsValid)
            {
                //create account
                ApplicationUser user = new ApplicationUser();
                user.Email = register.Email;
                user.PasswordHash = register.Password;
                user.UserName = register.UserName;
                IdentityResult result = await userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    //asign to role 
                    await userManager.AddToRoleAsync(user, "Worker");

                    return RedirectToAction("New", "Roles");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(register);
        }

    }
}
