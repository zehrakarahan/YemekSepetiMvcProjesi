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
    public class ProductManager : IProductService
    {
        private IProductDal _ProductDal;
        public ProductManager(IProductDal ProductDal)
        {
            _ProductDal = ProductDal;
        }
        public Product Add(Product entity)
        {
            return _ProductDal.Add(entity);
        }

        public void Delete(Product entity)
        {
            _ProductDal.Delete(entity);
        }

        public Product FirmaBilgisi(int firmaId)
        {
            return _ProductDal.Get(x => x.CompaniesId == firmaId);
        }

        public Product Get(int Id)
        {
            return _ProductDal.Get(x => x.Id == Id);
        }

        public List<Product> GetAll()
        {
            return _ProductDal.GetAll();
        }

        public Product Getpromosyon(int promosyonid,int firmaid)
        {
            return _ProductDal.Get(x => x.PromosyonId == promosyonid && x.CompaniesId==firmaid);
        }

        public List<Product> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public List<Product> Geturun(int firmaId)
        {
            return _ProductDal.GetAll(x => x.CompaniesId == firmaId);
        }

        public Product Update(Product entity)
        {
            return _ProductDal.Update(entity);  
        }

        public Product Getirbilgi(int firmaid, string urunadi, int urunid)
        {
            return _ProductDal.Get(x=>x.CompaniesId==firmaid && x.Pname==urunadi && x.Id==urunid);
        }
    }
}
