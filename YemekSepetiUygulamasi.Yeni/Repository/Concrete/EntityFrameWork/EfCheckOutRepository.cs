using MarketE_Commerce_Site.Entity;
using MarketE_Commerce_Site.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Repository.Concrete.EntityFrameWork
{
    public class EfCheckOutRepository : EfGenericRepository<CheckOut>,ICheckOutRepository
    {
        public EfCheckOutRepository(EcommerceDbContext context) : base(context) 
        {

        }
        public EcommerceDbContext EcommerceDbContext
        {
            get { return EcommerceDbContext as EcommerceDbContext; }
        }


    }
}
