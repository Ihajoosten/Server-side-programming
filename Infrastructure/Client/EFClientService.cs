using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Client
{
    public class EFClientService : IClientService
    {
        private readonly ClientDbContext _context;

        public EFClientService(ClientDbContext context) => _context = context;

        public IQueryable<Domain.Client> Client => _context.Client;

        public void CreateClient(Domain.Client client)
        {
            if (client == null)
            {
                _context.Client.Add(client);
            }
            else
            {
                Domain.Client entry = _context.Client.FirstOrDefault(p => p.Email == client.Email);
                if (entry != null)
                {
                    entry.FirstName = client.FirstName;
                    entry.LastName = client.LastName;
                    entry.Email = client.Email;
                    entry.Birthday = client.Birthday;
                    entry.City = client.City;
                    entry.Street = client.Street;
                    entry.HouseNumber = client.HouseNumber;
                    entry.Addition = client.Addition;
                    entry.PostalCode = client.PostalCode;
                    entry.Gluten = client.Gluten;
                    entry.Diabetes = client.Diabetes;
                    entry.Salt = client.Salt;
                };
            }
            _context.SaveChanges();
        }
    }
}
