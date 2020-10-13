using MarketE_Commerce_Site.IdentityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Models
{
    public class UserAndRoleModels
    {
        public string UserAndRoleModelsId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public bool EmailConfirmed { get; set; }
        public string RoleName { get; set; }
        public string PhoneNumber { get; set; }







    }
}
