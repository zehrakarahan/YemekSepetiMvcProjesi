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
    public class OrderProductManager : IOrderProductService
    {
        private IOrderProductDal _OrderProductDal;
        public OrderProductManager(IOrderProductDal OrderProductDal)
        {
            _OrderProductDal = OrderProductDal;
        }
        public OrderProduct Add(OrderProduct entity)
        {
            return _OrderProductDal.Add(entity);
        }

        public void Delete(OrderProduct entity)
        {
            _OrderProductDal.Delete(entity);
        }

        public OrderProduct Get(int Id)
        {
            return _OrderProductDal.Get(x => x.Id == Id);
        }

        public List<OrderProduct> GetAll()
        {
            return _OrderProductDal.GetAll();
        }

        public List<OrderProduct> Getir(int firmaId)
        {
            return _OrderProductDal.GetAll(x => x.CompaniesId == firmaId);
        }

        public OrderProduct Getirorder(int orderId)
        {
            return _OrderProductDal.Get(x => x.OrderId == orderId);
        }

        public List<OrderProduct> Getorder(int orderId)
        {
            return _OrderProductDal.GetAll(x => x.OrderId == orderId);
        }

        public List<OrderProduct> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<OrderProduct> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public List<OrderProduct> Sipariskampanya(int firmaId, int kampanyaId)
        {
            return _OrderProductDal.GetAll(x => x.CompaniesId == firmaId && x.CampaignProductId == kampanyaId);
        }

        public List<OrderProduct> Siparismenu(int firmaId, int menunameId)
        {
            return _OrderProductDal.GetAll(x => x.CompaniesId == firmaId && x.MenuNamesId == menunameId);
        }

        public List<OrderProduct> Siparisproduct(int firmaId, int productId)
        {
            return _OrderProductDal.GetAll(x => x.CompaniesId == firmaId && x.ProductId == productId);
        }

        public OrderProduct Update(OrderProduct entity)
        {
            return _OrderProductDal.Update(entity);
        }
    }
}
