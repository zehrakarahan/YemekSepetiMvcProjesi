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
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _CategoryDal;
        public CategoryManager(ICategoryDal CategoryDal)
        {
            _CategoryDal = CategoryDal;
        }

        public Category Add(Category entity)
        {
            return _CategoryDal.Add(entity);
        }

        public void Delete(Category entity)
        {
            _CategoryDal.Delete(entity);
        }

        public Category Get(int Id)
        {
           return _CategoryDal.Get(x=>x.Id==Id);
        }

        public List<Category> GetAll()
        {
            return _CategoryDal.GetAll();
        }

        public Category Getircategorisim(string categoriname)
        {
            return _CategoryDal.Get(x => x.Cname == categoriname);
        }

        public List<Category> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Category Update(Category entity)
        {
            return _CategoryDal.Update(entity);
        }
    }
}
