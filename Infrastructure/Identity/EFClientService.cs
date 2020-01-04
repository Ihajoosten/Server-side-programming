using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Identity
{
    public class EFClientService : IClientService
    {
        private readonly LoginDbContext _context;

        public EFClientService(LoginDbContext context) => _context = context;

        public IQueryable<Domain.Client> Client => _context.Client;

        public void UpdateClient(Domain.Client client)
        {
            try
            {
                _context.Client.Update(client);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
           
        }

        public Domain.Client GetClient(int? id)
        {
            var person = _context.Client.SingleOrDefault(c => c.Id == id);
            return person;
        }
    }
}
