using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltamiraTaskApi.Domain
{
    public partial class Address
    {
        //public Address()
        //{
        //    User = new HashSet<User>();
        //}

        public int Id { get; set; }
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }

        [ForeignKey("Geo")]
        public int? GeoId { get; set; }

        public virtual Geo Geo { get; set; }
 

        //public virtual ICollection<User> User { get; set; }
    }
}
