using Domain;
using DomainServices;
using System;
using System.Linq;

namespace Infrastructure.Identity
{
    public class EFCookService : ICookService
    {
        protected readonly LoginDbContext _context;

        public EFCookService(LoginDbContext context) => _context = context;

        public IQueryable<Domain.Cook> Cook => _context.Cook;

    }
}
