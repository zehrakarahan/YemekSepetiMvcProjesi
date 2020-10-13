using MarketE_Commerce_Site.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Components
{
    public class BestSeller : ViewComponent
    {
        private IProductRepository context;
        public BestSeller(IProductRepository _context)
        {
            context = _context;
        }
        public IViewComponentResult Invoke(int SlideNumber)
        {
            if (SlideNumber == 0)
            {
                ViewBag.IsActive = "False";
                return View(context.GetAll().Where(i => i.Bestsellers && i.productIsActive).ToList());
            }
            else
            {
                ViewBag.IsActive = "True";
                return View(context.GetAll().Where(i => i.IsNew && i.productIsActive).ToList());
            }
        }
    }
}
