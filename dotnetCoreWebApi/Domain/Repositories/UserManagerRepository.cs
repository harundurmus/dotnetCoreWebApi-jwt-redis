using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using AltamiraTaskApi.Security.Token;
using TokenOptions = AltamiraTaskApi.Security.Token.TokenOptions;

namespace AltamiraTaskApi.Domain.Repositories
{
    public class UserManagerRepository : BaseRepository, IUserManagerRepository
    {
        private readonly TokenOptions tokenOptions;

        public UserManagerRepository(AltamiraContext context, IOptions<TokenOptions> tokenOptions) : base(context)
        {
            this.tokenOptions = tokenOptions.Value;
        }

        public void AddUser(UserManager user)
        {
            context.UserManager.Add(user);
        }

        public UserManager FindByEmailandPassword(string email, string password)
        {
            return context.UserManager.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
        }

        public UserManager FindById(int userId)
        {
            return context.UserManager.Find(userId);
        }

        public UserManager GetUserWithRefreshToken(string refreshToken)
        {
            return context.UserManager.FirstOrDefault(u => u.RefreshToken == refreshToken);
        }

        public void RemoveRefreshToken(UserManager user_manager)
        {
            UserManager newUser = this.FindById(user_manager.Id);
            newUser.RefreshToken = null;
            newUser.RefreshTokenEndDate = null;
        }

       

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            UserManager newUser = this.FindById(userId);

            newUser.RefreshToken = refreshToken;
            newUser.RefreshTokenEndDate = DateTime.Now.AddMinutes(tokenOptions.RefreshTokenExpiration);
        }
    }
}
