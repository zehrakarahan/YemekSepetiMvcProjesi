using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Repository.Abstract
{
    public interface IUnitOfWorkRepository : IDisposable
    {
        ICategoryRepository Category{get;}
        IProductRepository Product { get; }
        ISubcategoryRepository Subcategory { get; }
        IMainSliderSetupRepostiory MainSliderSetup { get; }
        IMainPageSetupsRepository MainPageSetup { get; }
        ICheckOutRepository CheckOut{get;}
        ICheckOutLineRepository CheckOutLine { get; }
        int SaveChanges();
    }
}
