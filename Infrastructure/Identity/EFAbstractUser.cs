using Domain;
using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Identity
{
    public class EFAbstractUser : IAbstractUser
    {
        private readonly LoginDbContext _context;

        public EFAbstractUser(LoginDbContext context) => _context = context;

        public IQueryable<AbstractUser> User => _context.User;

        public List<AbstractUser> GetUsers() => _context.User.ToList();

        public AbstractUser GetUserByEmail(string email)
        {
            AbstractUser user = _context.User.FirstOrDefault(cl => cl.Email == email);
            return user;
        }

    }
}
