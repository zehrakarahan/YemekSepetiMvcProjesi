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
    public class CategoryTypeManager : ICategoryTypeService
    {
        private ICategoryTypeDal _CategoryTypeDal;
        public CategoryTypeManager(ICategoryTypeDal CategoryTypeDal)
        {
            _CategoryTypeDal = CategoryTypeDal;
        }

        public Categorytype Add(Categorytype entity)
        {
            return _CategoryTypeDal.Add(entity);
        }

        public void Delete(Categorytype entity)
        {
            _CategoryTypeDal.Delete(entity);
        }

        public Categorytype Get(int Id)
        {
            return _CategoryTypeDal.Get(x => x.CTypeID == Id);
        }

        public List<Categorytype> GetAll()
        {
            return _CategoryTypeDal.GetAll();
        }

        public List<Categorytype> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Categorytype> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Categorytype Update(Categorytype entity)
        {
            return _CategoryTypeDal.Update(entity);
        }
    }
}
