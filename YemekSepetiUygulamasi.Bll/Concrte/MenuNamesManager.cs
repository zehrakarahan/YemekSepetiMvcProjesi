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
    public class MenuNamesManager : IMenuNamesService
    {
        private IMenuNamesDal _MenuNamesDal;
        public MenuNamesManager(IMenuNamesDal MenuNamesDal)
        {
            _MenuNamesDal = MenuNamesDal;
        }
        public MenuNames Add(MenuNames entity)
        {
            return _MenuNamesDal.Add(entity);
        }

        public void Delete(MenuNames entity)
        {
            _MenuNamesDal.Delete(entity);
        }

        public MenuNames Get(int Id)
        {
            return _MenuNamesDal.Get2(x => x.Id == Id);
        }

        public List<MenuNames> GetAll()
        {
            return _MenuNamesDal.GetAll();
        }

        public List<MenuNames> Getir(int firmaId, int menuproductId)
        {
            return _MenuNamesDal.GetAll(x => x.CompaniesId == firmaId);
        }

        public MenuNames GetirBilgi(int firmaId, int Id)
        {
            return _MenuNamesDal.Get(x => x.CompaniesId == firmaId && x.Id == Id);
        }

        public List<MenuNames> Getirliste(int firmaId)
        {
            return _MenuNamesDal.GetAll(x=>x.CompaniesId==firmaId);
        }

        public List<MenuNames> Getirurunliste(int firmaId, int productId)
        {
            throw new NotImplementedException();
        }

        public MenuNames GetmenuBilgisi(int firmaid, string menuadi, int menuid)
        {
            return _MenuNamesDal.Get(x=>x.CompaniesId==firmaid && x.MenuName==menuadi && x.Id==menuid);
        }

        public List<MenuNames> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<MenuNames> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public MenuNames GetPromosyon(int firmaid, int promosyonId)
        {
            return _MenuNamesDal.Get(x => x.CompaniesId == firmaid && x.PromosyonId == promosyonId);
        }

        public MenuNames Update(MenuNames entity)
        {
            return _MenuNamesDal.Update(entity);
        }
    }
}
