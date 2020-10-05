using System;
using AltamiraTaskApi.Domain;
using AltamiraTaskApi.Domain.Repositories;
using AltamiraTaskApi.Domain.Responses;
using AltamiraTaskApi.Domain.Services;
using AltamiraTaskApi.Domain.UnitOfWork;

namespace AltamiraTaskApi.Services
{
    public class UserService : IUserManagerService
    {
        private readonly IUserManagerRepository userRepository;

        private readonly IUnitOfWork<AltamiraContext> unitOfWork;


        public UserService(IUserManagerRepository userRepository, IUnitOfWork<AltamiraContext> unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public BaseResponse<UserManager> AddUser(UserManager user)
        {
            try
            {
                userRepository.AddUser(user);
                unitOfWork.Complete();
                return new BaseResponse<UserManager>(user);
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserManager>($"Kullanıcı eklenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public BaseResponse<UserManager> FindById(int userId)
        {
            try
            {
                UserManager user = userRepository.FindById(userId);

                if (user == null)
                {
                    return new BaseResponse<UserManager>("Kullanıcı bulunamadı.");
                }

                return new BaseResponse<UserManager>(user);
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserManager>($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public BaseResponse<UserManager> FindEmailAndPassword(string email, string password)
        {
            try
            {
                UserManager user = userRepository.FindByEmailandPassword(email, password);
                if (user == null)
                {
                    return new BaseResponse<UserManager>("Kullanıcı bulunamadı.");
                }
                else
                {
                    return new BaseResponse<UserManager>(user);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserManager>($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public BaseResponse<UserManager> GetUserWithRefreshToken(string refreshToken)
        {
            try
            {
                UserManager user = userRepository.GetUserWithRefreshToken(refreshToken);

                if (user == null)
                {
                    return new BaseResponse<UserManager>("Kullanıcı bulunamadı.");
                }
                else
                {
                    return new BaseResponse<UserManager>(user);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserManager>($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public void RemoveRefreshToken(UserManager user)
        {
            try
            {
                userRepository.RemoveRefreshToken(user);
                unitOfWork.Complete();
            }
            catch (Exception)
            {
                //loglama yapılacaktır.
            }
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            try
            {
                userRepository.SaveRefreshToken(userId, refreshToken);

                unitOfWork.Complete();
            }
            catch (Exception)
            {
                //loglama yapılacaktır..
            }
        }
    }
}
