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

        void CreateClient(Client client);


    }
}
