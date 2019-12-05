using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainServices
{
    public interface ICookService
    {
        IQueryable<Cook> Cook { get; }

        void CreateCook(Cook cook);

    }
}
