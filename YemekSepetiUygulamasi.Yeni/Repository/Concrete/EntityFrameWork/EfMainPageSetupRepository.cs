using MarketE_Commerce_Site.Entity;
using MarketE_Commerce_Site.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Repository.Concrete.EntityFrameWork
{
    public class EfMainPageSetupRepository :EfGenericRepository<MainPage>, IMainPageSetupsRepository
    {
        public EfMainPageSetupRepository(EcommerceDbContext context ) 
            :base (context)
        {


        }
        public EcommerceDbContext ecommerceDbContext
        {
            get { return context as EcommerceDbContext; }
        }
    }
}
