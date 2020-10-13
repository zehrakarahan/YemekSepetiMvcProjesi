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
    public class ManagerTableManager : IManagerTableService
    {
        private IManagerTableDal _managerTableDal;
        public ManagerTableManager(IManagerTableDal managerTableDal)
        {
            _managerTableDal = managerTableDal;
        }

        public ManagerTable Add(ManagerTable entity)
        {
            return _managerTableDal.Add(entity);
        }

        public void Delete(ManagerTable entity)
        {
            _managerTableDal.Delete(entity);
        }

        public ManagerTable Get(string Kullanici,string sifre)
        {
            return _managerTableDal.Get(x => x.Mname == Kullanici && x.Password==sifre);
        }

        public ManagerTable Get(int sepetId)
        {
            return _managerTableDal.Get(x => x.Id == sepetId);
        }

        public List<ManagerTable> GetAll()
        {
            return _managerTableDal.GetAll();
        }

        public ManagerTable Getir(string emailadresi)
        {
            return _managerTableDal.Get(x => x.EmailAdresi == emailadresi);
        }

        public List<ManagerTable> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<ManagerTable> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public ManagerTable Update(ManagerTable entity)
        {
            return _managerTableDal.Update(entity);
        }

        public ManagerTable Geti(string Kullanici)
        {
            return _managerTableDal.Get(x => x.Mname == Kullanici);
        }
    }
}
