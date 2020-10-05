using AltamiraTaskApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Security.Token
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(UserManager user);

        void RevokeRefreshToken(UserManager user);
    }
}
