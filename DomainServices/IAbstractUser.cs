using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainServices
{
    public interface IAbstractUser
    {
        IQueryable<AbstractUser> User { get; }

        List<AbstractUser> GetUsers();

    }
}
