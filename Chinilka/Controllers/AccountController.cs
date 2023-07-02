using Chinilka.Models.Entities;
using Chinilka.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chinilka.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ChinilkaUser> userManager;
        private SignInManager<ChinilkaUser> signInManager;

        public AccountController(UserManager<ChinilkaUser> userMgr, SignInManager<ChinilkaUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        public ViewResult Login(string returnUrl) => View(new LoginModel { ReturnUrl = returnUrl });
        public ViewResult Register(string returnUrl) => View(new RegisterModel { ReturnUrl = returnUrl });

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ChinilkaUser { 
                    Email = model.Email, 
                    Login = model.Login , 
                    UserName = model.UserName, 
                    PhoneNumber = model.PhoneNumber 
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user,"User");
                    await signInManager.SignInAsync(user, false);
                    return Redirect(model.ReturnUrl ?? "/");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);

                if (result.Succeeded)
                {
                    return Redirect(model.ReturnUrl ?? "/");
                }

                ModelState.AddModelError("", "Неверный логин или пароль");
            }
            return View(model);
        }

        [Authorize]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}
