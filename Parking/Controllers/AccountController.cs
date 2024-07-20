using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Parking.Models;
using Parking.viewmodels;
using System.Data;
using System.Security.Claims;

namespace Parking.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser>signInManager) 
        {
            this.signInManager = signInManager;
            this.userManager=userManager;
        }

        //[HttpGet]
        //public IActionResult Addadmin()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task< IActionResult> Addadmin(RegisterUserViewModel register)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //create account
        //        ApplicationUser user = new ApplicationUser();
        //        user.Email = register.Email;
        //        user.PasswordHash = register.Password;
        //        user.UserName = register.UserName;
        //        IdentityResult result = await userManager.CreateAsync(user, register.Password);
        //        if (result.Succeeded)
        //        {
        //            //assign to role 
        //            await userManager.AddToRoleAsync(user, "Admin");
        //            //create cookie
        //            await signInManager.SignInAsync(user, false);
        //            return RedirectToAction("New", "Roles");
        //        }
        //        else
        //        {
        //            foreach (var item in result.Errors)
        //            {
        //                ModelState.AddModelError("", item.Description);
        //            }
        //        }

        //    }
        //    return View(register);
        //}

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Register(RegisterUserViewModel register)
        {
            if(ModelState.IsValid)
            {
                //create account
                ApplicationUser user = new ApplicationUser();   
                user.Email = register.Email;
                user.PasswordHash=register.Password;
                user.UserName = register.UserName;
               IdentityResult result =await userManager.CreateAsync(user,register.Password);
                if (result.Succeeded)
                {
                    //asign to role 
                    await userManager.AddToRoleAsync(user, "User");
                    
                    // creat cliams 
                    var cliams = new List<Claim>();
                    cliams.Add(new Claim(ClaimTypes.Name, user.UserName));
                    cliams.Add(new Claim(ClaimTypes.Email, user.Email));
                    cliams.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    cliams.Add(new Claim(ClaimTypes.Role, "User"));
                    //create cookie
                    await signInManager.SignInWithClaimsAsync(user, false,cliams);
                    return RedirectToAction("Display", "User");
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


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user =  await userManager.FindByEmailAsync(login.Email);
                if(user != null)
                {
                    bool found=await userManager.CheckPasswordAsync(user, login.Password);
                    if(found)
                    {
                        
                        var roles = await userManager.GetRolesAsync(user);
                        // creat cliams 
                        var cliams=new List<Claim>();
                        cliams.Add(new Claim(ClaimTypes.Name, user.UserName));
                        cliams.Add(new Claim(ClaimTypes.Email, user.Email));
                        cliams.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        // Add role to  claims
                        foreach (var role in roles)
                        {
                            cliams.Add(new Claim(ClaimTypes.Role, role));
                        }

                        await signInManager.SignInWithClaimsAsync(user,false,cliams);


                        if (roles.Contains("Admin"))
                        {
                            
                            return RedirectToAction("New", "Roles");
                        }
                        else if (roles.Contains("Worker"))
                        {
                            return RedirectToAction( "Upload", "Worker");
                        }
                        else
                        {
                            return RedirectToAction( "Display", "User");
                        }
                    }
                }
                ModelState.AddModelError("", "email and password invalid");
            }
            return View();  
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
