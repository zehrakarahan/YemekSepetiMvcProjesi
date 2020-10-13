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
    public class SalesManager : ISalesService
    {
        private ISalesDal _SalesDal;
        public SalesManager(ISalesDal SalesDal)
        {
            _SalesDal = SalesDal;
        }
        public Sales Add(Sales entity)
        {
            return _SalesDal.Add(entity);
        }

        public void Delete(Sales entity)
        {
            _SalesDal.Delete(entity);
        }

        public Sales Get(int Id)
        {
            return _SalesDal.Get(x => x.Id == Id);
        }

        public List<Sales> GetAll()
        {
            return _SalesDal.GetAll();
        }

        public List<Sales> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Sales> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Sales Update(Sales entity)
        {
            return _SalesDal.Update(entity);
        }
    }
}
