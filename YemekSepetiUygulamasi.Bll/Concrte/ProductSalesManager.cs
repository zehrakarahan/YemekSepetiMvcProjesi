using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;
using YemekSepetiUygulamasi.Bll.Abstract;
using YemekSepetiUygulamasi.Dal.Abstract;

namespace YemekSepetiUygulamasi.Bll.Concrte
{
    public class ProductSalesManager : IProductSalesService
    {
        private IProductSalesDal _ProductSalesDal;
        public ProductSalesManager(IProductSalesDal ProductSalesDal)
        {
            _ProductSalesDal = ProductSalesDal;
        }
        public ProductSales Add(ProductSales entity)
        {
            return _ProductSalesDal.Add(entity);
        }

        public void Delete(ProductSales entity)
        {
            _ProductSalesDal.Delete(entity);
        }

        public ProductSales Get(int Id)
        {
            return _ProductSalesDal.Get(x => x.Id == Id);
        }

        public List<ProductSales> GetAll()
        {
            return _ProductSalesDal.GetAll();
        }

        public List<ProductSales> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<ProductSales> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public ProductSales Update(ProductSales entity)
        {
            return _ProductSalesDal.Update(entity);
        }
    }
}
