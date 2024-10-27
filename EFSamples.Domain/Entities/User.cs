using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EFSamples.Domain.Entities
{
    public class User:BaseEntity
    {
        public string FirstName { get; set; }=null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public Address Home { get; set; }
        public Address? WorkPlace { get; set; }

        public ICollection<Order> Orders { get; set; }

    }

    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string MoreInfo { get; set; }
    }

}
