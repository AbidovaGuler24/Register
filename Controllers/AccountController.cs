using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Pronia.Helpers.Enums;
using Pronia.Models;
using Pronia.ViewModels.Account;

namespace Pronia.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

       

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
            _userManager.AddToRoleAsync(user, UserRoler.Admin.ToString());
            return RedirectToAction("Login");

            
        }
       
        
        public async Task<IActionResult> Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = await _userManager.FindByEmailAsync(LoginVm.EmailOrUserName);
           ??  await _userManager.FindByNameAsync(LoginVm.EmailOrUserName);

            if (user is null)
            {
                ModelState.AddModelError("", "EmailOrUserName or Password Sehvdir ");


                 return View();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, LoginVm.Password, true);

           if (result.IsLockOut)
            {
                ModelState.AddModelError("", " Password Sehvdir ");
                return RedirectToAction("Index", "Home");
            }
            if (!result.Succeeded) 
            {
                ModelState.AddModelError("", "EmailOrUserName or Password Sehvdir ");


                return View();  
            }

            await _signInManager.SignInAsync(user, LoginVm.Reminder);

           
                return RedirectToAction();
        }

        public async  Task<IActionResult> CreateRole()
        {
            foreach(var item in Enum.GetValues(typeof(UserRoler)))
            {
                await _roleManager.CreateAsync(new IdentityRole(){
                    Name=item.ToString()
                });
            }
           // await _roleManager.CreateAsync(new IdentityRole()
           // {
           //     Name="Admin"
           // });
           //await _roleManager.CreateAsync(new IdentityRole()
           // {
           //     Name = "Member"
           // });
            return RedirectToAction("Index", "Home");
        }
    }
}
