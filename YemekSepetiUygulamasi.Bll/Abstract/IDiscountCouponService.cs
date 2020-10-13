using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IDiscountCouponService
    {
        List<DiscountCoupon> GetAll();
        List<DiscountCoupon> GetProductsByOgrenciId(int OgrenciId);
        List<DiscountCoupon> GetProductsByOgrenciName(string ogrenciName);
        DiscountCoupon Get(int Id);
        List<DiscountCoupon> Getir(int firmaid);
        DiscountCoupon Update(DiscountCoupon entity);
        void Delete(DiscountCoupon entity);
        DiscountCoupon Add(DiscountCoupon entity);
    }
}
