using AltamiraTaskApi.Domain;
using AltamiraTaskApi.Domain.Repositories;
using AltamiraTaskApi.Domain.Responses;
using AltamiraTaskApi.Domain.Services;
using AltamiraTaskApi.Domain.UnitOfWork;
using AltamiraTaskApi.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> genericRepository;
        private readonly IUnitOfWork<AltamiraContext> unitOfWork;

        public GenericService(IGenericRepository<T> genericRepository, IUnitOfWork<AltamiraContext> unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<T>> Add(T entry)
        {
            try
            {
                await this.genericRepository.Add(entry);
                await this.unitOfWork.CompleteAsync();
                return new BaseResponse<T>(entry);
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }

        public async Task<int> CountWhere(Expression<Func<T, bool>> predicate)
        {
            return await genericRepository.CountWhere(predicate);
        }

        public async Task<BaseResponse<T>> Delete(int id)
        {
            try
            {
                T t = await this.genericRepository.GetById(id);

                if (t != null)
                {
                    await this.genericRepository.Delete(id);
                    await this.unitOfWork.CompleteAsync();
                    return new BaseResponse<T>(t);
                }
                else
                {
                    return new BaseResponse<T>("id sahip satır bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }

        public async Task<BaseResponse<T>> GetById(int id)
        {
            try
            {
                T t = await this.genericRepository.GetById(id);

                if (t != null)
                {
                    return new BaseResponse<T>(t);
                }
                else
                {
                    return new BaseResponse<T>("id sahip satır bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<T>>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> t = await this.genericRepository.GetWhere(predicate);

            return new BaseResponse<IEnumerable<T>>(t);
        }

        public  List<GetUsersResorce> GetWhereUserResource(IEnumerable<User> userListResponse)
        {
            List<GetUsersResorce> resources = new List<GetUsersResorce>();
            foreach (var item in userListResponse)
            {
                GetCompany cmp = new GetCompany()
                {
                    Bs = item.Company.Bs,
                    Name = item.Company.Name,
                    CatchPhrase = item.Company.CatchPhrase
                };
                GetGeo ge = new GetGeo()
                {
                    Lat = item.Address.Geo.Lat,
                    Lng = item.Address.Geo.Lng,
                };
                GetAddress adrs = new GetAddress()
                {
                    City = item.Address.City,
                    Street = item.Address.Street,
                    Suite = item.Address.Suite,
                    Zipcode = item.Address.Zipcode,
                    getGeo = ge,
                };
                GetUsersResorce userResource = new GetUsersResorce()
                {
                    id = item.Id,
                    address = adrs,
                    company = cmp,
                    email = item.Email,
                    name = item.Name,
                    phone = item.Phone,
                    username = item.Username,
                    website = item.Website
                };

                resources.Add(userResource);
            }

            return resources;
        }
        public GetUsersResorce GetWhereUserResourceOne(User u)
        {
            
                GetCompany cmp = new GetCompany()
                {
                    Bs = u.Company.Bs,
                    Name = u.Company.Name,
                    CatchPhrase = u.Company.CatchPhrase
                };
                GetGeo ge = new GetGeo()
                {
                    Lat = u.Address.Geo.Lat,
                    Lng = u.Address.Geo.Lng,
                };
                GetAddress adrs = new GetAddress()
                {
                    City = u.Address.City,
                    Street = u.Address.Street,
                    Suite = u.Address.Suite,
                    Zipcode = u.Address.Zipcode,
                    getGeo = ge,
                };
                GetUsersResorce userResource = new GetUsersResorce()
                {
                    id = u.Id,
                    address = adrs,
                    company = cmp,
                    email = u.Email,
                    name = u.Name,
                    phone = u.Phone,
                    username = u.Username,
                    website = u.Website
                };

            return userResource;
        }

        public async Task<BaseResponse<T>> Update(T entry)
        {
            try
            {
                this.genericRepository.Update(entry);
                await this.unitOfWork.CompleteAsync();
                return new BaseResponse<T>(entry);
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }
    }
}
