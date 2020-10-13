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
    public class CountyManager : ICountyService
    {
        private ICountyDal _CountyDal;
        public CountyManager(ICountyDal CountyDal)
        {
            _CountyDal = CountyDal;
        }

        public County Add(County entity)
        {
            return _CountyDal.Add(entity);
        }

        public void Delete(County entity)
        {
            _CountyDal.Delete(entity);
        }

        public County Get(int Id)
        {
            return _CountyDal.Get(x => x.Id == Id);
        }

        public List<County> GetAll()
        {
            return _CountyDal.GetAll();
        }

        public List<County> Getir(int CityId)
        {
            return _CountyDal.GetAll(x => x.CityId == CityId);
        }

      

        public List<County> Getiril(int ilId)
        {
            return _CountyDal.GetAll(x => x.CityId == ilId);
        }

        public List<County> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<County> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public County Update(County entity)
        {
            return _CountyDal.Update(entity);
        }
    }
}
