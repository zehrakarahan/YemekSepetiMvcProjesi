using MarketE_Commerce_Site.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Repository.Concrete.EntityFrameWork
{
    public class EfUnitOfWork : IUnitOfWorkRepository
    {
        public EcommerceDbContext dbContext;
        public EfUnitOfWork(EcommerceDbContext _dbContext)
        {
            dbContext = _dbContext ?? throw new ArgumentNullException("Dbcontext is not be null!!");
        }
        private IProductRepository _products;
        private ICategoryRepository _categories;
        private ISubcategoryRepository _subcategories;
        private IMainSliderSetupRepostiory _mainSliderSetup;
        private IMainPageSetupsRepository _mainPageSetup;
        private ICheckOutLineRepository _checkOutLineSetup;
        private ICheckOutRepository _checkOutSetup;

        public ICategoryRepository Category
        {
            get
            {
                return _categories ?? (_categories = new EfCategoryRepository(dbContext));
            }
        }

        public IProductRepository Product
        {
            get
            {
                return _products ?? (_products = new EfProductRepository(dbContext));
            }
        }

        public ISubcategoryRepository Subcategory
        {
            get
            {
                return _subcategories ?? (_subcategories = new EfSubCategoriesRepository(dbContext));
            }
        }


        public IMainSliderSetupRepostiory MainSliderSetup
        {
            get
            {
                return _mainSliderSetup ?? (_mainSliderSetup = new EfMainSliderSetup(dbContext));
            }
        }

        public IMainPageSetupsRepository MainPageSetup
        {
            get
            {
                return _mainPageSetup ?? (_mainPageSetup = new EfMainPageSetupRepository(dbContext));
            }
        }

        public ICheckOutRepository CheckOut
        {
            get
            {
                return _checkOutSetup ?? (_checkOutSetup = new EfCheckOutRepository(dbContext));
            }
        }

        public ICheckOutLineRepository CheckOutLine
        {
            get
            {
                return _checkOutLineSetup ?? (_checkOutLineSetup = new EfCheckOutLineRepository(dbContext));
            }
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }
    }
}
