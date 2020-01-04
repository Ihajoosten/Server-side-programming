using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class LoginDbContext : IdentityDbContext<Domain.Cook>
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options) { }

        public DbSet<Domain.Cook> Cook { get; set; }
        public DbSet<Domain.Client> Client { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Domain.Cook>();
            modelBuilder.Entity<Domain.Client>();
        }
    }
}
