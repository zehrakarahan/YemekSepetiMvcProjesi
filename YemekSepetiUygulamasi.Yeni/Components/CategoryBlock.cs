using MarketE_Commerce_Site.Entity;
using MarketE_Commerce_Site.Models;
using MarketE_Commerce_Site.Repository.Abstract;
using MarketE_Commerce_Site.Repository.Concrete.EntityFrameWork;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Components
{
    public class CategoryBlock : ViewComponent
    {
        public IUnitOfWorkRepository uow;
        public EcommerceDbContext contex;
        public CategoryBlock(IUnitOfWorkRepository _uow, EcommerceDbContext _contex)
        {
            uow = _uow;
            contex = _contex;
        }

        public IViewComponentResult Invoke()
        {
            List<CategoryModel> CategoryListCount = new List<CategoryModel>();


            var categoryCounts =
                 from p in contex.Products
                 join c in contex.Categories
                 on p.CategoryId equals c.CategoriesId
                 group c by new { c.CategoriesId, c.CategoriesName } into g
                 select
                 new
                 {
                     Category = g.Key.CategoriesId,
                     Productname = g.Key.CategoriesName,
                     ProductCount = g.Count()
                 };
            //var hede = categoryCounts.ToList();
            foreach (var item in categoryCounts.ToList())
            {
                var ListCategory = new CategoryModel()
                {
                    CategoryId = item.Category,
                    CategoryName = item.Productname,
                    Count = item.ProductCount,
                };
                CategoryListCount.Add(ListCategory);

            }
            return View(CategoryListCount);

        }


    }
}
