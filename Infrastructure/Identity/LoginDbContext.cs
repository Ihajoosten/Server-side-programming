using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Infrastructure.Identity
{
    public class LoginDbContext : IdentityDbContext<AbstractUser>
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options) { }


        public DbSet<AbstractUser> User { get; set; }

        public DbSet<Domain.Client> Cook { get; set; }

        public DbSet<Domain.Cook> Client { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AbstractUser>();
            modelBuilder.Entity<Domain.Client>();
            modelBuilder.Entity<Domain.Cook>();
        }
    }
}
