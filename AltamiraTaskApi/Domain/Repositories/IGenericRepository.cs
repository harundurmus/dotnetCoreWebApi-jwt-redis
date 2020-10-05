using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);

        Task<int> CountWhere(Expression<Func<T, bool>> predicate);

        Task Add(T entry); // asekron geriye birşey dönmeyecekseniz task

        void Update(T entry);

        Task Delete(int id);

    }
}
