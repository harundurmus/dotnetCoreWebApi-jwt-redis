using AltamiraTaskApi.Domain.Responses;
using AltamiraTaskApi.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Domain.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<BaseResponse<T>> GetById(int id);
        Task<BaseResponse<IEnumerable<T>>> GetWhere(Expression<Func<T, bool>> predicate);
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);
        Task<BaseResponse<T>> Add(T entry); 
        Task<BaseResponse<T>> Update(T entry);
        Task<BaseResponse<T>> Delete(int id);

        List<GetUsersResorce> GetWhereUserResource(IEnumerable<User> users);
        GetUsersResorce GetWhereUserResourceOne(User u);
    }
}
