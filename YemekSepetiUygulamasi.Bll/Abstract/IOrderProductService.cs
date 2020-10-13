using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IOrderProductService
    {
        List<OrderProduct> GetAll();
        List<OrderProduct> GetProductsByOgrenciId(int OgrenciId);
        List<OrderProduct> GetProductsByOgrenciName(string ogrenciName);
        OrderProduct Get(int Id);
        List<OrderProduct> Getir(int firmaId);
        OrderProduct Getirorder(int orderId);
        List<OrderProduct> Getorder(int orderId);
        List<OrderProduct> Siparisproduct(int firmaId, int productId);
        List<OrderProduct> Siparismenu(int firmaId, int menunameId);
        List<OrderProduct> Sipariskampanya(int firmaId, int kampanyaId);
        OrderProduct Update(OrderProduct entity);
        void Delete(OrderProduct entity);
        OrderProduct Add(OrderProduct entity);
    }
}
