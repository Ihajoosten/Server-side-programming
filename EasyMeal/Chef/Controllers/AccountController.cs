using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chef.Models.Account;
using Infrastructure.Chef;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chef.Controllers
{
    public class AccountController : Controller
    {
        //public IActionResult Login() => View("Login");

        private UserManager<ChefIdentity> userManager;
        private SignInManager<ChefIdentity> signInManager;

        public AccountController(UserManager<ChefIdentity> userMgr,
                SignInManager<ChefIdentity> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            //IdentitySeedData.EnsurePopulated(userMgr).Wait();
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                ChefIdentity user =
                    await userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,
                            loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect(loginModel?.ReturnUrl ?? "/Home/Index");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}