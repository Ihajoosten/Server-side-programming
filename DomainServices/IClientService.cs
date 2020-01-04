using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainServices
{
    public interface IClientService
    {

        IQueryable<Client> Client { get; }

        // As a Client I want to update my data if needed
        void UpdateClient(Client client);

        Client GetClient(int? id);
    }
}
