using MarketE_Commerce_Site.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Repository.Concrete.EntityFrameWork
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
               : base(options)
        {

        }
        public DbSet<CheckOutLine> CheckOutLines { get; set; }
        public DbSet<CheckOut> CheckOuts { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<SubCategories> SubCategories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<MainSliderSetup> MainSliderSetups { get; set; }
        public DbSet<MainPage> MainPageSetups { get; set; }
        

    }

  
}
