using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IPaymentsOptionsService
    {
        List<PaymentsOptions> GetAll();
        List<PaymentsOptions> GetProductsByOgrenciId(int OgrenciId);
        List<PaymentsOptions> GetProductsByOgrenciName(string ogrenciName);
        PaymentsOptions Get(int Id);
        PaymentsOptions Update(PaymentsOptions entity);
        void Delete(PaymentsOptions entity);
        PaymentsOptions Add(PaymentsOptions entity);
    }
}
