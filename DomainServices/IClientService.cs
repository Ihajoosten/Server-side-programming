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

        // As a Client I need to fetch my data to see account details
        Client GetClientById(int? id);

        // As a new Client I want to register myself
        void CreateClient(Client client);

        // As a Client I want to update my data if needed
        void UpdateClient(int? id);

        // As a Client I want to delete my account if needed
        void DeleteClient(int? id);

    }
}
