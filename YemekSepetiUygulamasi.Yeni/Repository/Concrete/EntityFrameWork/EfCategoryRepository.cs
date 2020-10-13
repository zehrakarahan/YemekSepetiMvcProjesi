using MarketE_Commerce_Site.Entity;
using MarketE_Commerce_Site.Models;
using MarketE_Commerce_Site.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Repository.Concrete.EntityFrameWork
{
    public class EfCategoryRepository : EfGenericRepository<Categories> , ICategoryRepository
    {
        public EfCategoryRepository(EcommerceDbContext context):base(context)
        {

        }
        public EcommerceDbContext EcommerceDbContext
        {
            get{ return context as EcommerceDbContext; }
        } // method yazılacaksa burası ıcın bura kullanılıcak

    }
}

