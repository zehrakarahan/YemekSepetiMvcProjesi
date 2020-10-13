using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Models
{
    public class CreateNewUser
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

    }
}
