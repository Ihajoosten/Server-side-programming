using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Domain.Client> _userManager;
        private readonly SignInManager<Domain.Client> _signInManager;

        public AccountController(UserManager<Domain.Client> userManager, SignInManager<Domain.Client> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // LOGIN Account
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        // POST login account
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Dashboard", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login!");
            }

            return View(model);
        }


        // REGISTER Account
        public IActionResult Register()
        {
            return View();
        }

        // POST register account
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Domain.Client
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    UserName = model.Email,
                    Email = model.Email,
                    Birthday = model.Birthday,
                    City = model.City,
                    Street = model.Street,
                    HouseNumber = model.HouseNumber,
                    Addition = model.Addition,
                    PostalCode = model.PostalCode
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}
