using System;
using System.Collections.Generic;

namespace AltamiraTaskApi.Domain
{
    public partial class Company
    {
        //public Company()
        //{
        //    User = new HashSet<User>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string CatchPhrase { get; set; }
        public string Bs { get; set; }

      
        //public virtual ICollection<User> User { get; set; }
    }
}
