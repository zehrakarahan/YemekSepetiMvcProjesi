using MarketE_Commerce_Site.Entity;
using MarketE_Commerce_Site.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Repository.Concrete.EntityFrameWork
{
    public class EfMainSliderSetup : EfGenericRepository<MainSliderSetup>, IMainSliderSetupRepostiory
    {
        public EfMainSliderSetup(EcommerceDbContext context) : base(context)
        {

        }
        public EcommerceDbContext EcommerceDbContext
        {
            get { return context as EcommerceDbContext; }
        }
    }
}
