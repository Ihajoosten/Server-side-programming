using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Chef.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() => View("Login");
    }
}