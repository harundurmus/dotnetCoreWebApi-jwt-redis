using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Domain.UnitOfWork
{
    public interface IUnitOfWork<T>
    {
        Task CompleteAsync();
        void Complete();
    }
}
