using System;
using authapi.entity;
using authapi.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace authapi.Controllers
{
    public class UserController:Controller
    {
        readonly UserManager<AppUser>_userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager=userManager;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var appUser=new AppUser
                {
                    UserName=model.Username,
                    Email=model.Email,
                };
                
                IdentityResult result= await _userManager.CreateAsync(appUser,model.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}
