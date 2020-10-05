using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Domain.Repositories
{
    public class BaseRepository
    {
        protected readonly AltamiraContext context;

        public BaseRepository(AltamiraContext context)
        {
            this.context = context;
        }
    }
}
