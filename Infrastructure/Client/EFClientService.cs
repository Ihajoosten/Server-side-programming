using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Client
{
    public class EFClientService : IClientService
    {
        private readonly ClientDbContext _context;

        public EFClientService(ClientDbContext context) => _context = context;

        public IQueryable<Domain.Client> Client => _context.Client;

        public void CreateClient(Domain.Client client)
        {
            _context.Client.Add(client);
            _context.SaveChanges();
        }
    }
}
