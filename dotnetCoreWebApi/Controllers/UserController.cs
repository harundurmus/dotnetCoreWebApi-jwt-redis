using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AltamiraTaskApi.Domain;
using AltamiraTaskApi.Domain.Repositories;
using AltamiraTaskApi.Domain.Responses;
using AltamiraTaskApi.Domain.Services;
using AltamiraTaskApi.Extensions;
using AltamiraTaskApi.Resources;
using AltamiraTaskApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace AltamiraTaskApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericService<User> userServices;
        private readonly IGenericService<Company> companyServices;
        private readonly IGenericService<Address> addressServices;
        private readonly IGenericService<Geo> geoServices;
        private ICacheService _cacheService;
        private readonly IMapper mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;


        public UserController(IGenericService<User> userServices, IGenericService<Company> companyServices, IGenericService<Address> addressServices,
            IGenericService<Geo> geoServices,
            IMapper mapper, ICacheService cacheService,
            IServiceScopeFactory serviceScopeFactory)
        {
            this.userServices = userServices;
            this.addressServices = addressServices;
            this.companyServices = companyServices;
            this.geoServices = geoServices;
            this.mapper = mapper;
            //this._redisService = redisService;
            //db = _redisService.GetDb(1);
            _cacheService = cacheService;

            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            if (_cacheService.Any("users"))
            {
                var users = _cacheService.Get<List<GetUsersResorce>>("users");
                return Ok(users);
            }
            else
            {
                BaseResponse<IEnumerable<User>> userListResponse = await userServices.GetWhere(x => x.Id > 0);

                if (userListResponse.Success)
                {
                    var result = userServices.GetWhereUserResource(userListResponse.Extra);

                    _cacheService.Add("users", result);

                    return Ok(result);
                }
                else
                {
                    return BadRequest(userListResponse.ErrorMessage);
                }
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFindById(int id)
        {

            if (_cacheService.Any("users_id" + id.ToString()))
            {
                var user = _cacheService.Get<GetUsersResorce>("users_id" + id.ToString());
                return Ok(user);
            }
            else
            {
                BaseResponse<User> UserResponse = await userServices.GetById(id);

                if (UserResponse.Success)
                {
                    var result = userServices.GetWhereUserResourceOne(UserResponse.Extra);

                    _cacheService.Add("users_id:" + result.id.ToString(), result);

                    return Ok(result);
                }
                else
                {
                    return BadRequest(UserResponse.ErrorMessage);
                }
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserResource userResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                User user = mapper.Map<UserResource, User>(userResource);
                var Response = await userServices.Add(user);

                if (Response.Success)
                {
                    _cacheService.Remove("users");
                    return Ok(Response.Extra);
                }
                else
                {
                    return BadRequest(Response.ErrorMessage);
                }
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(UserResource userResource, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                BaseResponse<User> UserResponse = await userServices.GetById(id);
                if (UserResponse.Success)
                {
                    User user = mapper.Map<UserResource, User>(userResource);

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<AltamiraContext>();
                        var _suser = dbContext.User.Find(id);

                        Company company = new Company()
                        {
                            Id = _suser.Company.Id,
                            Bs = userResource.company.Bs,
                            Name = userResource.company.Name,
                            CatchPhrase = userResource.company.CatchPhrase
                        };
                        var cres = await companyServices.Update(company);
                        Geo geo = new Geo()
                        {
                            Id = _suser.Address.GeoId.Value,
                            Lat = userResource.address.Geo.Lat,
                            Lng = userResource.address.Geo.Lng,
                        };
                        var cgeo = await geoServices.Update(geo);
                        Address address = new Address()
                        {
                            Id = _suser.AddresId,
                            City = userResource.address.City,
                            Street = userResource.address.Street,
                            Suite = userResource.address.Suite,
                            Zipcode = userResource.address.Zipcode,
                            GeoId = _suser.Address.GeoId
                        };
                        var cadres = await addressServices.Update(address);

                        user.CompanyId = _suser.CompanyId;
                        user.AddresId = _suser.AddresId;
                    }

                    user.Id = id;

                    var response = await userServices.Update(user);

                    if (response.Success)
                    {
                        _cacheService.Remove("users");
                        if (_cacheService.Any("users_id" + id.ToString()))
                        {
                            _cacheService.Remove("users_id:" + id.ToString());
                        }
                        var result = userServices.GetWhereUserResourceOne(response.Extra);
                        _cacheService.Add("users_id:" + result.id.ToString(), result);
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(response.ErrorMessage);
                    }
                }
                else
                {
                    return BadRequest(UserResponse.ErrorMessage);
                }

            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            BaseResponse<User> response = await userServices.Delete(id);

            if (response.Success)
            {
                _cacheService.Remove("users");
                _cacheService.Remove("users_id:" + id.ToString());
                return Ok("kullanıcı başarılı bir şekilde silindi.");
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }
        }

        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }

    }
}