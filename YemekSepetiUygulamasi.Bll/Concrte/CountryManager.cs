using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Bll.Abstract;
using YemekSepetiUygulamasi.Dal.Abstract;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Concrte
{
    public class CountryManager: ICountryService
    {
        private ICountryDal _CountryDal;
        public CountryManager(ICountryDal CountryDal)
        {
            _CountryDal = CountryDal;
        }

        public Country Add(Country entity)
        {
            return _CountryDal.Add(entity);
        }

        public void Delete(Country entity)
        {
            _CountryDal.Delete(entity);
        }

        public Country Get(int Id)
        {
            return _CountryDal.Get(x => x.Id == Id);
        }

        public List<Country> GetAll()
        {
            return _CountryDal.GetAll();
        }

        public List<Country> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Country> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Country Update(Country entity)
        {
            return _CountryDal.Update(entity);
        }
    }
}
