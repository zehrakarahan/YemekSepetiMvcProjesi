using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Bll.Abstract;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Dal.Abstract;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Concrte
{
    public class BasketManager : IBasketService
    {
        private IBasketDal _BasketDal;
        public BasketManager(IBasketDal BasketDal)
        {
            _BasketDal = BasketDal;
        }

        public Basket Add(Basket entity)
        {
            return _BasketDal.Add(entity);
        }

        public void Delete(Basket entity)
        {
            _BasketDal.Delete(entity);
        }

        public Basket Get(int sepetId)
        {
            return _BasketDal.Get(x=>x.Id==sepetId);
        }

        public List<Basket> GetAll()
        {
            return _BasketDal.GetAll();
        }

        public List<Basket> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Basket> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Basket Update(Basket entity)
        {
            return _BasketDal.Update(entity);
        }
    }
}
