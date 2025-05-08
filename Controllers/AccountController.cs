using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Pronia.Models;
using Pronia.ViewModels.Account;

namespace Pronia.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }

            AppUser appUser = new AppUser()
            {
                Name = registerVm.Name,
                Email = registerVm.Email,
                UserName = registerVm.UserName,
                Surname = registerVm.Surname,
            };

            var result= await _userManager.CreateAsync(appUser,registerVm.Password);

            if (!result.Succeeded) 
            {
                return View();
            }
            await _signInManager.SignInAsync(appUser,true);

            return RedirectToAction("Index","Home");

            
        }
        
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return View("Index", "Home");
        }    
    }
}
