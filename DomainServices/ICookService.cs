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

        // As a new Cook I want to register myself
        void CreateCook(Cook cook);

    }
}
