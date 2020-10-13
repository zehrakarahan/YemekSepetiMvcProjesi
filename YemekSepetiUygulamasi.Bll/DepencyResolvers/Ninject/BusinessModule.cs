using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Bll.Abstract;
using YemekSepetiUygulamasi.Bll.Concrte;
using YemekSepetiUygulamasi.Dal.Abstract;
using YemekSepetiUygulamasi.Dal.Concrete.EntityFramework.Repository;

namespace YemekSepetiUygulamasi.Bll.DepencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBasketService>().To<BasketManager>().InSingletonScope();
            Bind<IBasketDal>().To<BasketDal>().InSingletonScope();

            Bind<ICampaignPNameService>().To<CampaignPNameManager>().InSingletonScope();
            Bind<ICampaignPNameDal>().To<CampaignPNameDal>().InSingletonScope();

            Bind<IDiscountCouponService>().To<DiscountCouponManager>().InSingletonScope();
            Bind<IDiscountCouponDal>().To<DiscountCouponDal>().InSingletonScope();

            Bind<ICampaignProductsService>().To<CampaignProductsManager>().InSingletonScope();
            Bind<ICampaignProductsDal>().To<CampaignProductsDal>().InSingletonScope();

            Bind<IPromosyonService>().To<PromosyonManager>().InSingletonScope();
            Bind<IPromosyonDal>().To<PromosyonDal>().InSingletonScope();

            Bind<IManagerTableService>().To<ManagerTableManager>().InSingletonScope();
            Bind<IManagerTableDal>().To<ManagerTableDal>().InSingletonScope();

            Bind<IAuthorityService>().To<AuthorityManager>().InSingletonScope();
            Bind<IAuthorityDal>().To<AuthorityDal>().InSingletonScope();

            Bind<ICategoryService>().To<CategoryManager>().InSingletonScope();
            Bind<ICategoryDal>().To<CategoryDal>().InSingletonScope();

            Bind<ICategoryTypeDal>().To<CategoryTypeDal>().InSingletonScope();
            Bind<ICategoryTypeDal>().To<CategoryTypeDal>().InSingletonScope();

            Bind<ICityService>().To<CityManager>().InSingletonScope();
            Bind<ICityDal>().To<CityDal>().InSingletonScope();

            Bind<ICommentService>().To<CommentManager>().InSingletonScope();
            Bind<ICommentDal>().To<CommentDal>().InSingletonScope();

            Bind<ICompaniesService>().To<CompaniesManager>().InSingletonScope();
            Bind<ICompaniesDal>().To<CompaniesDal>().InSingletonScope();

            Bind<ICountyService>().To<CountyManager>().InSingletonScope();
            Bind<ICountyDal>().To<CountyDal>().InSingletonScope();

            Bind<ICountryService>().To<CountryManager>().InSingletonScope();
            Bind<ICountryDal>().To<CountryTableDal>().InSingletonScope();

            Bind<ICustomersAdressService>().To<CustomersAdressManager>().InSingletonScope();
            Bind<ICustomersAdressDal>().To<CustomersAdressDal>().InSingletonScope();

            Bind<ICustomerService>().To<CustomerManager>().InSingletonScope();
            Bind<ICustomerDal>().To<CustomerDal>().InSingletonScope();

            Bind<IDayTableService>().To<DayTableManager>().InSingletonScope();
            Bind<IDayTableDal>().To<DayTableDal>().InSingletonScope();

            Bind<IEmployeesService>().To<EmployeesManager>().InSingletonScope();
            Bind<IEmployeesDal>().To<EmployeesDal>().InSingletonScope();

            Bind<IFavoritesService>().To<FavoritesManager>().InSingletonScope();
            Bind<IFavoritesDal>().To<FavoritesDal>().InSingletonScope();

            Bind<IMenuNamesService>().To<MenuNamesManager>().InSingletonScope();
            Bind<IMenuNamesDal>().To<MenuNamesDal>().InSingletonScope();

            Bind<IMenusService>().To<MenusManager>().InSingletonScope();
            Bind<IMenusDal>().To<MenusDal>().InSingletonScope();

            Bind<INeighborhoodService>().To<NeighborhoodManager>().InSingletonScope();
            Bind<INeighborhoodDal>().To<NeighborhoodDal>().InSingletonScope();

            Bind<IOrderProductService>().To<OrderProductManager>().InSingletonScope();
            Bind<IOrderProductDal>().To<OrderProductDal>().InSingletonScope();

            Bind<IOrderService>().To<OrderManager>().InSingletonScope();
            Bind<IOrderDal>().To<OrderDal>().InSingletonScope();

            Bind<IPaymentsOptionsService>().To<PaymentsOptionsManager>().InSingletonScope();
            Bind<IPaymentsOptionsDal>().To<PaymentsOptionsDal>().InSingletonScope();

            Bind<IProductImageService>().To<ProductImageManager>().InSingletonScope();
            Bind<IProductImageDal>().To<ProductImageDal>().InSingletonScope();

            Bind<IProductService>().To<ProductManager>().InSingletonScope();
            Bind<IProductDal>().To<ProductDal>().InSingletonScope();

            Bind<IProductSalesService>().To<ProductSalesManager>().InSingletonScope();
            Bind<IProductSalesDal>().To<ProductSalesDal>().InSingletonScope();

            Bind<ISalesService>().To<SalesManager>().InSingletonScope();
            Bind<ISalesDal>().To<SalesDal>().InSingletonScope();

            Bind<ISandDtableService>().To<SandDtableManager>().InSingletonScope();
            Bind<ISandDtableDal>().To<SandDtableDal>().InSingletonScope();

            Bind<IShiftTableService>().To<ShiftTableManager>().InSingletonScope();
            Bind<IShiftTableDal>().To<ShiftTableDal>().InSingletonScope();

            Bind<ISliderService>().To<SliderManager>().InSingletonScope();
            Bind<ISliderDal>().To<SliderDal>().InSingletonScope();

            Bind<ISifreDegisiklikService>().To<SifreDegisiklikManager>().InSingletonScope();
            Bind<ISifreDegisiklikDal>().To<SifreDegisiklikDal>().InSingletonScope();

            Bind<IStatusService>().To<StatusManager>().InSingletonScope();
            Bind<IStatusDal>().To<StatusDal>().InSingletonScope();

            Bind<IWorkingService>().To<WorkingManager>().InSingletonScope();
            Bind<IWorkingDal>().To<WorkingDal>().InSingletonScope();



        }
    }
}
