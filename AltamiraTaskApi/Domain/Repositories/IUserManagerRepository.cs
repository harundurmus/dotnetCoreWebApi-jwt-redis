using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Domain.Repositories
{
    public interface IUserManagerRepository
    {
        void AddUser(UserManager user);

        UserManager FindById(int userId);

        UserManager FindByEmailandPassword(string email, string password);

        void SaveRefreshToken(int userId, string refreshToken);

        UserManager GetUserWithRefreshToken(string refreshToken);

        void RemoveRefreshToken(UserManager user);
    }
}
