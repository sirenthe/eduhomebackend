//using AspNetCore;
using System.Security.Cryptography.X509Certificates;
using EduHome.Identity;
using EduHome.Services.Interfaces;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EduHome.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService   _mailService;
        private readonly SignInManager<AppUser> _signInManager;
      


        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailService mailService)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
     
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return BadRequest("You are already registered.");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewmodel , string? returnUrl)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            if (User.Identity.IsAuthenticated)
            {
                return BadRequest("You are already registered.");
            }

            AppUser appuser = await _userManager.FindByNameAsync(loginViewmodel.UsernameorEmail); {
                if(appuser is  null)
                {
                    appuser = await _userManager.FindByEmailAsync(loginViewmodel.UsernameorEmail);
                    if(appuser is null)
                    {
                        ModelState.AddModelError("", "password 0r email is incorrect");
                        return View();
                    }
                }
                var signInresult = await _signInManager.PasswordSignInAsync(appuser, loginViewmodel.Password, loginViewmodel.RememberMe,false);
                if (!signInresult.Succeeded) {
                    ModelState.AddModelError("", "username or passowrd is false");
                        return View();
                }
                if(!appuser.LockoutEnabled)
                {
                    appuser.LockoutEnabled= true;
                  await   _userManager.UpdateAsync(appuser);
                    
                    appuser.LockoutEnd = null;
                }
                if (returnUrl is not null)
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            
        }
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("you haven't registered yet");
            }
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
        public IActionResult ForgotPassword()
        {
            return  View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotpasswordmodel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(forgotpasswordmodel.Email);
            if(user == null) {
                ModelState.AddModelError("Email", "user not found");
                return View();
                    }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Auth", new { email = forgotpasswordmodel.Email, token = token }, HttpContext.Request.Scheme);


         
            MailRequest mailRequest = new MailRequest
            {
                ToEmail = forgotpasswordmodel.Email,
                Subject = "Reset your password",
                Body = $"<a href='{link}'>reset password</a>"

            };
            await _mailService.SendEmailAsync(mailRequest);


            await _mailService.SendEmailAsync(mailRequest);
  
            return RedirectToAction(nameof(Login));
         
        }


        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel, string email, string token)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
                return BadRequest();

            if (!ModelState.IsValid)
                return View();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
                IdentityResult identityResult = await _userManager.ResetPasswordAsync(user, token, resetPasswordViewModel.NewPassword);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction("Login");
        }



    }
}

