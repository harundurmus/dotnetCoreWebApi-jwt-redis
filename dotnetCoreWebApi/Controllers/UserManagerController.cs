using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AltamiraTaskApi.Domain;
using AltamiraTaskApi.Domain.Responses;
using AltamiraTaskApi.Domain.Services;
using AltamiraTaskApi.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AltamiraTaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly IUserManagerService userService;
        private readonly IMapper mapper;

        public UserManagerController(IUserManagerService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUser()
        {
            IEnumerable<Claim> claims = User.Claims;

            string userId = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

            BaseResponse<UserManager> userResponse = userService.FindById(int.Parse(userId));

            if (userResponse.Success)
            {
                return Ok(userResponse.Extra);
            }
            else
            {
                return BadRequest(userResponse.ErrorMessage);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddUser(UserManagerResource userResource)
        {
            UserManager user = mapper.Map<UserManagerResource, UserManager>(userResource);
            BaseResponse<UserManager> userResponse = userService.AddUser(user);

            if (userResponse.Success)
            {
                return Ok(userResponse.Extra);
            }
            else
            {
                return BadRequest(userResponse.ErrorMessage);
            }
        }
    }
}