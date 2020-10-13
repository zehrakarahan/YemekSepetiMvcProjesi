using MarketE_Commerce_Site.Entity;
using MarketE_Commerce_Site.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Repository.Concrete.EntityFrameWork
{
    public class EfProductRepository : EfGenericRepository<Products>, IProductRepository
    {
        public EfProductRepository(EcommerceDbContext context) 
            : base(context)
        {


        }
        public EcommerceDbContext EcommerceDbContex
        {
            get { return context as EcommerceDbContext; }
        }
    }
}
