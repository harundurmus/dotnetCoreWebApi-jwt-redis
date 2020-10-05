using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltamiraTaskApi.Resources
{
    public class GetUsersResorce
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public virtual GetAddress address { get; set; }
        public virtual GetCompany company { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
    }


    public class GetAddress
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
       
        public virtual GetGeo getGeo { get; set; }
    }
    public class GetCompany
    {
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public string Bs { get; set; }
    }

    public class GetGeo
    {
        public string Lat { get; set; }
        public string Lng { get; set; }
    }
}
