using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IOrderService
    {
        List<Order> GetAll();
        List<Order> GetProductsByOgrenciId(int OgrenciId);
        List<Order> GetProductsByOgrenciName(string ogrenciName);
        Order Get(int Id);
      
        List<Order> Getir(int firmaId);
        Order Update(Order entity);
        void Delete(Order entity);
        Order Add(Order entity);
    }
}
