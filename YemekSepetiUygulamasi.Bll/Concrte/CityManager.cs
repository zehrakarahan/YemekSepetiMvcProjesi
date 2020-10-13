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
    public class CityManager : ICityService
    {
        private ICityDal _CityDal;
        public CityManager(ICityDal CityDal)
        {
            _CityDal = CityDal;
        }

        public City Add(City entity)
        {
            return _CityDal.Add(entity);  
        }

        public void Delete(City entity)
        {
            _CityDal.Delete(entity);
        }

        public City Get(int Id)
        {
            return _CityDal.Get(x => x.Id == Id);
        }

        public List<City> GetAll()
        {
            return _CityDal.GetAll();
        }

        public List<City> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<City> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public City Update(City entity)
        {
            return _CityDal.Update(entity);
        }
    }
}
