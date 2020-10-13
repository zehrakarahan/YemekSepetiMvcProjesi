using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Bll.Abstract;
using YemekSepetiUygulamasi.Dal.Abstract;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Concrte
{
    public class DiscountCouponManager: IDiscountCouponService
    {
        private IDiscountCouponDal _discountCoupondal;
        public DiscountCouponManager(IDiscountCouponDal discountCoupondal)
        {
            this._discountCoupondal = discountCoupondal;
        }

        public DiscountCoupon Add(DiscountCoupon entity)
        {
            return _discountCoupondal.Add(entity);
        }

        public void Delete(DiscountCoupon entity)
        {
            _discountCoupondal.Delete(entity);
        }

        public DiscountCoupon Get(int Id)
        {
            return _discountCoupondal.Get(x => x.Id == Id);
        }

        public List<DiscountCoupon> GetAll()
        {
            return _discountCoupondal.GetAll();
        }

        public List<DiscountCoupon> Getir(int firmaid)
        {
            return _discountCoupondal.GetAll(x => x.CompaniesId == firmaid);
        }

        public List<DiscountCoupon> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<DiscountCoupon> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public DiscountCoupon Update(DiscountCoupon entity)
        {
            return _discountCoupondal.Update(entity);
        }
    }
}
