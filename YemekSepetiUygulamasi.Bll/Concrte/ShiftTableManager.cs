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
    public class ShiftTableManager : IShiftTableService
    {
        private IShiftTableDal _ShiftTableDal;
        public ShiftTableManager(IShiftTableDal ShiftTableDal)
        {
            _ShiftTableDal = ShiftTableDal;
        }
        public ShiftTable Add(ShiftTable entity)
        {
            return _ShiftTableDal.Add(entity);
        }

        public void Delete(ShiftTable entity)
        {
            _ShiftTableDal.Delete(entity);
        }

        public ShiftTable Get(int Id)
        {
            return _ShiftTableDal.Get(x =>x.Id == Id);
        }

        public List<ShiftTable> GetAll()
        {
            return _ShiftTableDal.GetAll();
        }

        public List<ShiftTable> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<ShiftTable> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public ShiftTable Update(ShiftTable entity)
        {
            return _ShiftTableDal.Update(entity);
        }
    }
}
