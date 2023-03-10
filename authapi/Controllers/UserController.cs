using System.Net.Mail;
using authapi.entity;
using authapi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Net;

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
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(AppUserViewModel model)
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
        public IActionResult Login(string? returnUrl)
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
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        return Redirect(TempData["returnUrl"].ToString());
                    }
                    else
                    {
                        await _userManager.AccessFailedAsync(user);
                        int failCount = await _userManager.GetAccessFailedCountAsync(user);
                        if (failCount >= 3)
                        {
                            await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(1)));
                            ModelState.AddModelError("X??ta", "3 yanl???? giri?? etdiniz!");
                        }
                        else
                        {
                            if (result.IsLockedOut)
                            {
                                ModelState.AddModelError("X??ta", "3 yanl???? giri?? etdiyiniz ??????n hesab??n??z 1 d??qiq?? m??dd??tind?? ba??l??d??r!");
                            }
                            else
                            {
                                ModelState.AddModelError("X??ta", "??stifad????i ad?? v?? ya ??ifr?? yanl????d??r!");
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("X??ta1", "??stifad????i t??yin oluna bilm??di");
                    ModelState.AddModelError("X??ta2", "??stifad????i ad?? v?? ya ??ifr?? yanl????d??r!");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/User/Index");
        }

        #region 
        // Password-un update olunmasinda problem var.Yeniden bax

        [HttpGet]
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("TestMest1122@outlook.com", "Testmest@1122");
                client.Port = 587; // 25 587
                client.Host = "smtp.office365.com";
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("TestMest1122@outlook.com", "ResetPassword");
                mail.To.Add(new MailAddress(model.Email));
                mail.Subject = "Reset password";
                mail.Body = $"<a target=\"_blank\" href=\"https://localhost:5001{Url.Action("UpdatePassword", "Password", new { userId = user.Id, token = HttpUtility.UrlEncode(resetToken) })}\">Click here!</a>".ToString();

                client.Send(mail);
                ViewBag.State = true;
            }
            else
            {
                ViewBag.State = false;
            }
            return View();
        }

        [HttpGet("[action]/{userId}/{token}")]
        public async Task<IActionResult> UpdatePassword()
        {
            return View();
        }

        [HttpPost("[action]/{userId}/{token}")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel model, string userId, string token)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), model.Password);
            if (result.Succeeded)
            {
                ViewBag.State = true;
                await _userManager.UpdateSecurityStampAsync(user);
            }
            else
                ViewBag.State = false;
            return View();
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.UserName = model.UserName;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(x => ModelState.AddModelError(x.Code, x.Description));
                    return View(model);
                }
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, true);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(EditPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (await _userManager.CheckPasswordAsync(user, model.OldPassword))
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (!result.Succeeded)
                    {
                        result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                        return View(model);
                    }
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, true);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
