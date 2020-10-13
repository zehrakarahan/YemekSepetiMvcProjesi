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
    public class ProductImageManager : IProductImageService
    {
        private IProductImageDal _ProductImageDal;
        public ProductImageManager(IProductImageDal ProductImageDal)
        {
            _ProductImageDal = ProductImageDal;
        }
        public ProductImage Add(ProductImage entity)
        {
            return _ProductImageDal.Add(entity);  
        }

        public void Delete(ProductImage entity)
        {
            _ProductImageDal.Delete(entity);
        }

        public ProductImage Get(int Id)
        {
            return _ProductImageDal.Get(x => x.Id == Id);
        }

        public List<ProductImage> GetAll()
        {
            return _ProductImageDal.GetAll();
        }

        public ProductImage Getir(int firmaId, int productId)
        {
            return _ProductImageDal.Get(x => x.CompaniesId == firmaId && x.ProductId == productId);
        }

        public ProductImage Getproduct(int productId)
        {
            return _ProductImageDal.Get(x => x.ProductId == productId);
        }

        public List<ProductImage> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<ProductImage> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public ProductImage Update(ProductImage entity)
        {
            return _ProductImageDal.Update(entity);
        }
    }
}
