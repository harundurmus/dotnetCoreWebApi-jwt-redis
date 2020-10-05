using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltamiraTaskApi.Domain
{
    public partial class Geo
    {
        //public Geo()
        //{
        //    Address = new HashSet<Address>();
        //}

        public int Id { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }

       
        //public virtual ICollection<Address> Address { get; set; }
    }
}
