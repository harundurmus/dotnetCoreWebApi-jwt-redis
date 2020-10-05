using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltamiraTaskApi.Domain
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        [ForeignKey("Address")]
        public int AddresId { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Company Company { get; set; }
    }
}
