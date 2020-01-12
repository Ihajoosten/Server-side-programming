using System.Threading.Tasks;
using Cook.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Infrastructure.Identity;
using DomainServices;

namespace Cook.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AbstractUser> _userManager;
        private readonly SignInManager<AbstractUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAbstractUser _abstractUserService;

        public AccountController(IAbstractUser abstractUser, UserManager<AbstractUser> userManager, SignInManager<AbstractUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _abstractUserService = abstractUser;
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
                var allUsers = _abstractUserService.GetUsers();

                AbstractUser getUser = new AbstractUser();

                foreach (var item in allUsers)
                {
                    if (item.Email == model.Email) getUser = item;
                }

                if (await _userManager.IsInRoleAsync(getUser, "Cook"))
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Dashboard", "Home");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login!");
                }
                else if (await _userManager.IsInRoleAsync(getUser, "Client"))
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "You are not authorized to visit this website");
                    }
                }
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
                var user = new AbstractUser
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