using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.IdentityCore
{
    public class ApplicationUsers :IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
