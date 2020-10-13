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
    public class SandDtableManager : ISandDtableService
    {
        private ISandDtableDal _SandDtableDal;
        public SandDtableManager(ISandDtableDal SandDtableDal)
        {
            _SandDtableDal = SandDtableDal;
        }
        public SandDtable Add(SandDtable entity)
        {
            return _SandDtableDal.Add(entity);
        }

        public void Delete(SandDtable entity)
        {
            _SandDtableDal.Delete(entity);
        }

        public SandDtable Get(int Id)
        {
            return _SandDtableDal.Get(x => x.Id == Id);
        }

        public List<SandDtable> GetAll()
        {
            return _SandDtableDal.GetAll();
        }

        public List<SandDtable> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<SandDtable> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public SandDtable Update(SandDtable entity)
        {
            return _SandDtableDal.Update(entity);
        }
    }
}
