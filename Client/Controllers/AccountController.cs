using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Models.Account;
using DomainServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Domain.Client> _userManager;
        private readonly SignInManager<Domain.Client> _signInManager;
        private readonly IClientService _service;

        public AccountController(UserManager<Domain.Client> userManager, SignInManager<Domain.Client> signInManager, IClientService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _service = service;
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
                    PostalCode = model.PostalCode,
                    Gluten = model.Gluten,
                    Diabetes = model.Diabetes,
                    Salt = model.Salt
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

        public async Task<IActionResult> Update()
        {
            var client = await _userManager.FindByEmailAsync("lucjoosten1234@outlook.com");
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Domain.Client client)
        {
            if (ModelState.IsValid)
            {
                // Get the existing student from the db
                var user = await _userManager.FindByEmailAsync(client.Email);

                user.FirstName = client.FirstName;
                user.LastName = client.LastName;
                user.PhoneNumber = client.PhoneNumber;
                user.Birthday = client.Birthday;
                user.PostalCode = client.PostalCode;
                user.Street = client.Street;
                user.City = client.City;
                user.Addition = client.Addition;
                user.HouseNumber = client.HouseNumber;
                user.Password = client.Password;
                user.ConfirmPassword = client.ConfirmPassword;
                user.Gluten = client.Gluten;
                user.Diabetes = client.Diabetes;
                user.Salt = client.Salt;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(client);
        }
    }
}
