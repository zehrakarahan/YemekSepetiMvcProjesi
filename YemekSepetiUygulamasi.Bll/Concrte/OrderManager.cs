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
    public class OrderManager : IOrderService
    {
        private IOrderDal _OrderDal;
        public OrderManager(IOrderDal OrderDal)
        {
            _OrderDal = OrderDal;
        }
        public Order Add(Order entity)
        {
            return _OrderDal.Add(entity);
        }

        public void Delete(Order entity)
        {
             _OrderDal.Delete(entity);
        }

        public Order Get(int Id)
        {
            return _OrderDal.Get(x => x.Id == Id);
        }

        public List<Order> GetAll()
        {
            return _OrderDal.GetAll();
        }

        public List<Order> Getir(int firmaId)
        {
            return _OrderDal.GetAll(x => x.CompaniesId == firmaId);
        }

        public List<Order> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

  
        public Order Update(Order entity)
        {
            return _OrderDal.Update(entity); ;
        }
    }
}
