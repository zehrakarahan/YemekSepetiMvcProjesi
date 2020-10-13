using MarketE_Commerce_Site.IdentityCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Components
{
    public class TitleBody : ViewComponent
    {
        private ApplicationIdentityDbContext context;

        public TitleBody(ApplicationIdentityDbContext _context)
        {
            context = _context;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.userList =  context.Users.ToList().Count();
            return View();

        }
    }
}
