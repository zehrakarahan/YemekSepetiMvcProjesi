using MarketE_Commerce_Site.Entity;
using MarketE_Commerce_Site.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Repository.Concrete.EntityFrameWork
{
    public class EfSubCategoriesRepository : EfGenericRepository<SubCategories>, ISubcategoryRepository
    {
        public EfSubCategoriesRepository(EcommerceDbContext context) 
            : base(context)
        {
        }
        public EcommerceDbContext EcommerceDbContext
        {
            get { return context as EcommerceDbContext; }
        }
    }
}
