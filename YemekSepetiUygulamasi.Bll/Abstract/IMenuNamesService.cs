using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IMenuNamesService
    {
        List<MenuNames> GetAll();
        List<MenuNames> GetProductsByOgrenciId(int OgrenciId);
        List<MenuNames> GetProductsByOgrenciName(string ogrenciName);
        MenuNames Get(int Id);
        MenuNames Update(MenuNames entity);
        void Delete(MenuNames entity);
        MenuNames Add(MenuNames entity);
        MenuNames GetmenuBilgisi(int firmaid, string menuadi, int menuid);
        MenuNames GetPromosyon(int firmaid, int promosyonId);
        List<MenuNames> Getir(int firmaId, int menuproductId);
        List<MenuNames> Getirurunliste(int firmaId, int productId);
        List<MenuNames> Getirliste(int firmaId);
        MenuNames GetirBilgi(int firmaId, int Id);
    }
}
