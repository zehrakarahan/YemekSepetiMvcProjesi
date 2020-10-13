using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetProductsByOgrenciId(int OgrenciId);
        List<Product> GetProductsByOgrenciName(string ogrenciName);
        Product Get(int Id);
        Product Getirbilgi(int firmaid, string urunadi, int urunid);
        Product Getpromosyon(int promosyonid,int firmaid);
        Product FirmaBilgisi(int firmaId);
        List<Product> Geturun(int firmaId);
        Product Update(Product entity);
        void Delete(Product entity);
        Product Add(Product entity);
    }
}
