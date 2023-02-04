using System;
using authapi.entity;
using authapi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace authapi.Controllers
{
    public class UserController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;

        public UserController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        [Authorize]
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
            if (ModelState.IsValid)
            {
                var appUser = new AppUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                Console.WriteLine(result);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.password, model.persistent, model.islocked);
                    if (result.Succeeded)
                        return Redirect(TempData["returnUrl"].ToString());
                }
                else
                {
                    ModelState.AddModelError("Xəta1", "İstifadəçi təyin oluna bilmədi");
                    ModelState.AddModelError("Xəta2", "İstifadəçi adı və ya şifrə yanlışdır!");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View(TempData["returnUrl"].ToString());
        }
    }
}
