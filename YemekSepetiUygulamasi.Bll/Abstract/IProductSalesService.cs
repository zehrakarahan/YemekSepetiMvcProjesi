using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IProductSalesService
    {
        List<ProductSales> GetAll();
        List<ProductSales> GetProductsByOgrenciId(int OgrenciId);
        List<ProductSales> GetProductsByOgrenciName(string ogrenciName);
        ProductSales Get(int Id);
        ProductSales Update(ProductSales entity);
        void Delete(ProductSales entity);
        ProductSales Add(ProductSales entity);
    }
}
