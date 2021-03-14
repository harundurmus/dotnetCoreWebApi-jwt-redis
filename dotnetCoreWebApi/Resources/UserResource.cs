using AltamiraTaskApi.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Resources
{
    public class UserResource
    {
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public virtual Address address { get; set; }
        public virtual Company company { get; set; }
        //public virtual Geo geo { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
   
    }
}
