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
    public class DayTableManager : IDayTableService
    {
        private IDayTableDal _DayTableDal;
        public DayTableManager(IDayTableDal DayTableDal)
        {
            _DayTableDal = DayTableDal;
        }
        public DayTable Add(DayTable entity)
        {
            return _DayTableDal.Add(entity);
        }

        public void Delete(DayTable entity)
        {
            _DayTableDal.Delete(entity);
        }

        public DayTable Get(int Id)
        {
            return _DayTableDal.Get(x => x.DayID == Id);
        }

        public List<DayTable> GetAll()
        {
            return _DayTableDal.GetAll();
        }

        public List<DayTable> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<DayTable> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public DayTable Update(DayTable entity)
        {
            return _DayTableDal.Update(entity);
        }
    }
}
