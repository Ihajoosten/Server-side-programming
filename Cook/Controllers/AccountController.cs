using System.Threading.Tasks;
using Cook.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace Cook.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AbstractUser> _userManager;
        private readonly SignInManager<AbstractUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AbstractUser> userManager, SignInManager<AbstractUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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
            return View();
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
                var user = new Domain.Cook
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    UserName = model.Email,
                    Email = model.Email
                };
                var roleExist = _roleManager.RoleExistsAsync("Cook").Result;
                if (!roleExist)
                {
                    //create the roles and seed them to the database
                    IdentityRole Cook = new IdentityRole()
                    {
                        Name = "Cook"
                    };
                    await _roleManager.CreateAsync(Cook);
                }

                var accountResult = await _userManager.CreateAsync(user, model.Password);

                if (accountResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Cook");
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in accountResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}