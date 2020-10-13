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
    public class PaymentsOptionsManager : IPaymentsOptionsService
    {
        private IPaymentsOptionsDal _PaymentsOptionsDal;
        public PaymentsOptionsManager(IPaymentsOptionsDal PaymentsOptionsDal)
        {
            _PaymentsOptionsDal = PaymentsOptionsDal;
        }
        public PaymentsOptions Add(PaymentsOptions entity)
        {
            return _PaymentsOptionsDal.Add(entity);
        }

        public void Delete(PaymentsOptions entity)
        {
            _PaymentsOptionsDal.Delete(entity);
        }

        public PaymentsOptions Get(int Id)
        {
            return _PaymentsOptionsDal.Get(x => x.Id == Id);
        }

        public List<PaymentsOptions> GetAll()
        {
            return _PaymentsOptionsDal.GetAll();
        }

        public List<PaymentsOptions> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<PaymentsOptions> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public PaymentsOptions Update(PaymentsOptions entity)
        {
            return _PaymentsOptionsDal.Update(entity);
        }
    }
}
