using AutoMapper;
using EduHome.Identity;
using EduHome.Services.Interfaces;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;

        public AccountController(IMapper mapper, UserManager<AppUser> userManager, IMailService mailService)
        {   
        _mapper = mapper;
            _userManager = userManager;
            _mailService = mailService;
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return BadRequest("You are already registered.");
            }

            return View();
        }
 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (User.Identity.IsAuthenticated)
            {
                return BadRequest("You are already registered.");
            }
            AppUser newUser = _mapper.Map<AppUser>(registerViewModel);
            newUser.IsActive = true;

            IdentityResult identityResult = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "user not found");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Auth", new { email = registerViewModel.Email, token = token }, HttpContext.Request.Scheme);



            MailRequest mailRequest = new MailRequest
            {
                ToEmail = registerViewModel.Email,
                Subject = "Reset your password",
                Body = $"<a href='{link}'>reset password</a>"

            };
            await _mailService.SendEmailAsync(mailRequest);


            await _mailService.SendEmailAsync(mailRequest);
            user.EmailConfirmed= true;
            return RedirectToAction("Login", "Auth"); 
        }
    }
}
