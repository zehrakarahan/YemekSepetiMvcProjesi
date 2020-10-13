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
    public class WorkingManager : IWorkingService
    {
        private IWorkingDal _WorkingDal;
        public WorkingManager(IWorkingDal WorkingDal)
        {
            _WorkingDal = WorkingDal;
        }
        public Working Add(Working entity)
        {
            return _WorkingDal.Add(entity); 
        }

        public void Delete(Working entity)
        {
            _WorkingDal.Delete(entity);
        }

        public Working Get(int Id)
        {
            return _WorkingDal.Get(x => x.Id == Id);
        }

        public List<Working> GetAll()
        {
            return _WorkingDal.GetAll();
        }

        public List<Working> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Working> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Working Update(Working entity)
        {
            return _WorkingDal.Update(entity);
        }
    }
}
