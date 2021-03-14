using AltamiraTaskApi.Domain;
using AltamiraTaskApi.Resources;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Mapping
{
    public class UserManagerMapping : Profile
    {
        public UserManagerMapping()
        {
            CreateMap<UserManagerResource, UserManager>();
            CreateMap<UserManager, UserManagerResource>();
        }
    }
}
