using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Resources
{
    public class UserManagerResource
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string SurName { get; set; }
    }
}
