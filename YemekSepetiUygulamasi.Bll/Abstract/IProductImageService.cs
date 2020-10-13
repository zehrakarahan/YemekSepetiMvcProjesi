using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IProductImageService
    {
        List<ProductImage> GetAll();
        List<ProductImage> GetProductsByOgrenciId(int OgrenciId);
        List<ProductImage> GetProductsByOgrenciName(string ogrenciName);
        ProductImage Get(int Id);
        ProductImage Getproduct(int productId);
        ProductImage Getir(int firmaId, int productId);
        ProductImage Update(ProductImage entity);
        void Delete(ProductImage entity);
        ProductImage Add(ProductImage entity);
    }
}
