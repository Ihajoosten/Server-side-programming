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

        Client GetClientById(int? id);

        void CreateClient(Client client);

        void UpdateClient(int? id);

        void DeleteClient(int? id);

    }
}
