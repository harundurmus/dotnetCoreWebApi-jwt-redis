using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Domain.Initializer
{
    interface IDbInitializer
    {
        void Initialize();

        void SeedData();
    }
}
