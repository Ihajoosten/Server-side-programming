using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Collections.Generic;

namespace Infrastructure.Identity
{
    public class LoginDbContext : IdentityDbContext<AbstractUser, IdentityRole, string>
    {

        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options) { }


        public DbSet<AbstractUser> User { get; set; }

    }
}
