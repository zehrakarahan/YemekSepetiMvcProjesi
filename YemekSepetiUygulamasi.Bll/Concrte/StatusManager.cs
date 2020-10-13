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
    public class StatusManager : IStatusService
    {
        private IStatusDal _StatusDal;
        public StatusManager(IStatusDal StatusDal)
        {
            _StatusDal = StatusDal;
        }
        public Status Add(Status entity)
        {
            return _StatusDal.Add(entity);
        }

        public void Delete(Status entity)
        {
            _StatusDal.Delete(entity);
        }

        public Status Get(int Id)
        {
            return _StatusDal.Get(x => x.Id == Id);
        }

        public List<Status> GetAll()
        {
            return _StatusDal.GetAll();
        }

        public List<Status> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Status> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Status Update(Status entity)
        {
            return _StatusDal.Update(entity);
        }
    }
}
