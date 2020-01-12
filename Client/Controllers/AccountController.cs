using Domain;
using System.Threading.Tasks;
using Client.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DomainServices;
using System.Linq;
using System.Diagnostics;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AbstractUser> _userManager;
        private readonly SignInManager<AbstractUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAbstractUser _abstractUserService;
        private readonly IClientService _clientService;

        public AccountController(IAbstractUser userService, IClientService service, UserManager<AbstractUser> userManager, SignInManager<AbstractUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _abstractUserService = userService;
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

        [HttpGet]
        public IActionResult Update()
        {
            // Retrieve Identity user
            var allUsers = _abstractUserService.GetUsers();

            AbstractUser getUser = new AbstractUser();

            foreach (var item in allUsers)
            {
                if (item.Email == User.Identity.Name) getUser = item;
            }

            // Retrieve Client user
            Domain.Client client = _clientService.Client.FirstOrDefault(c => c.Email == getUser.Email);

            // Arrage return model
            UpdateViewModel model = new UpdateViewModel()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Birthday = client.Birthday,
                City = client.City,
                Street = client.Street,
                HouseNumber = client.HouseNumber,
                Addition = client.Addition,
                PostalCode = client.PostalCode,
                Gluten = client.Gluten,
                Diabetes = client.Diabetes,
                Salt = client.Salt,
                Password = getUser.Password,
                ConfirmPassword = getUser.ConfirmPassword
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve Identity user
                var user = _abstractUserService.GetUserByEmail(User.Identity.Name);

                if (user == null) ModelState.AddModelError(string.Empty, "Dit email is nog niet bekend in het systeem dus uw account kan niet worden geupdate");

                // Retrieve Client user
                var client = _clientService.GetClientByEmail(User.Identity.Name);

                if (client == null) ModelState.AddModelError(string.Empty, "Dit email is nog niet bekend in het systeem dus uw account kan niet worden geupdate");

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.ConfirmPassword = model.ConfirmPassword;
                user.UserName = model.Email;
                user.Email = model.Email;

                client.FirstName = model.FirstName;
                client.LastName = model.LastName;
                client.Email = model.Email;
                client.Birthday = model.Birthday;
                client.City = model.City;
                client.Street = model.Street;
                client.HouseNumber = model.HouseNumber;
                client.Addition = model.Addition;
                client.PostalCode = model.PostalCode;
                client.Gluten = model.Gluten;
                client.Diabetes = model.Diabetes;
                client.Salt = model.Salt;

                var x = await _userManager.ChangePasswordAsync(user, user.Password, model.Password);

                if (x.Succeeded) user.Password = model.Password;


                var result = await _userManager.UpdateAsync(user);
                _clientService.UpdateClient(client);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Failed to update your account!");

            }
            return View(model);
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
                AbstractUser user = _abstractUserService.GetUserByEmail(model.Email);

                if (await _userManager.IsInRoleAsync(user, "Client"))
                {

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Your password or email does not match");

                }
                else if (await _userManager.IsInRoleAsync(user, "Cook"))
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
                var client = new Domain.Client
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
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

                _clientService.CreateClient(client);

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
