using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Domain.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : DbContext
    {
        private readonly T context;

        public UnitOfWork(T context)
        {
            this.context = context;
            //this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }

        public void Complete()
        {
            this.context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
