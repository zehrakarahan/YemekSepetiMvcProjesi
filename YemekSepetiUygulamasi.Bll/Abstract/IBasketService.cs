using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IBasketService
    {
        List<Basket> GetAll();
        List<Basket> GetProductsByOgrenciId(int OgrenciId);
        List<Basket> GetProductsByOgrenciName(string ogrenciName);
        Basket Get(int sepetId);
        Basket Update(Basket entity);
        void Delete(Basket entity);
        Basket Add(Basket entity);

    }
}
