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
    public class NeighborhoodManager : INeighborhoodService
    {
        private INeighborhoodDal _NeighborhoodDal;
        public NeighborhoodManager(INeighborhoodDal NeighborhoodDal)
        {
            _NeighborhoodDal = NeighborhoodDal;
        }
        public Neighborhood Add(Neighborhood entity)
        {
            return _NeighborhoodDal.Add(entity);
        }

        public void Delete(Neighborhood entity)
        {
            _NeighborhoodDal.Delete(entity);
        }

        public Neighborhood Get(int Id)
        {
            return _NeighborhoodDal.Get(x => x.Id == Id);
        }

        public List<Neighborhood> GetAll()
        {
            return _NeighborhoodDal.GetAll();
        }

        public List<Neighborhood> Getir(int countyId)
        {
            return _NeighborhoodDal.GetAll(x => x.CountyId == countyId);
        }

        public List<Neighborhood> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Neighborhood> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Neighborhood Update(Neighborhood entity)
        {
            return _NeighborhoodDal.Update(entity);
        }
    }
}
