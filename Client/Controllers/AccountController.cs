using Domain;
using System.Threading.Tasks;
using Client.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DomainServices;
using System.Security.Claims;
using Infrastructure.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AbstractUser> _userManager;
        private readonly SignInManager<AbstractUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IClientService _clientService;
        private LoginDbContext _context;

        public AccountController(LoginDbContext context, IClientService service, UserManager<AbstractUser> userManager, SignInManager<AbstractUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _clientService = service;
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
            return View("Login");
        }

        // POST login account
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var allUsers = _context.getUsers;

                AbstractUser getUser = new AbstractUser();

                foreach (var item in allUsers)
                {
                    if (item.Email == model.Email) getUser = item;
                }

                if (await _userManager.IsInRoleAsync(getUser, "Client"))
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid Login!");
                }
                else if (await _userManager.IsInRoleAsync(getUser, "Cook"))
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "You are not authorized to visit this website, please register as a new customer!");
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
                //var client = new Domain.Client
                //{
                //    FirstName = model.FirstName,
                //    LastName = model.LastName,
                //    Email = model.Email,
                //    Birthday = model.Birthday,
                //    City = model.City,
                //    Street = model.Street,
                //    HouseNumber = model.HouseNumber,
                //    Addition = model.Addition,
                //    PostalCode = model.PostalCode,
                //    Gluten = model.Gluten,
                //    Diabetes = model.Diabetes,
                //    Salt = model.Salt                    
                //};

                var user = new AbstractUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    UserName = model.Email,
                    Email = model.Email
                };

                var roleExist = _roleManager.RoleExistsAsync("Client").Result;
                if (!roleExist)
                {
                    //create the roles and seed them to the database
                    IdentityRole Client = new IdentityRole()
                    {
                        Name = "Client"
                    };
                    await _roleManager.CreateAsync(Client);
                }

                var result = await _userManager.CreateAsync(user, model.Password);
                //_clientService.CreateClient(client);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Client");

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
