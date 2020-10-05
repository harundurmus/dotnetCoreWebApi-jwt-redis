using AltamiraTaskApi.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Domain.Services
{
    public interface IUserManagerService
    {
        BaseResponse<UserManager> AddUser(UserManager user);

        BaseResponse<UserManager> FindById(int userId);

        BaseResponse<UserManager> FindEmailAndPassword(string email, string password);

        void SaveRefreshToken(int userId, string refreshToken);

        BaseResponse<UserManager> GetUserWithRefreshToken(string refreshToken);

        void RemoveRefreshToken(UserManager user);
    }
}
