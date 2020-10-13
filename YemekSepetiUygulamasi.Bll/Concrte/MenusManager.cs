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
    public class MenusManager : IMenusService
    {
        private IMenusDal _MenusDal;
        public MenusManager(IMenusDal MenusDal)
        {
            _MenusDal = MenusDal;
        }
        public Menus Add(Menus entity)
        {
            return _MenusDal.Add(entity);
        }

        public void Delete(Menus entity)
        {
            _MenusDal.Delete(entity);
        }

        public Menus Get(int Id)
        {
            return _MenusDal.Get(x => x.Id == Id);
        }

        public Menus Get(int firmaId, int menunamesId)
        {
            return _MenusDal.Get(x => x.CompaniesId == firmaId && x.MenuNamesId == menunamesId);
        }

        public List<Menus> GetAll()
        {
            return _MenusDal.GetAll();
        }

     

        public List<Menus> Getir(int firmaId, int menusId)
        {
            return _MenusDal.GetAll(x => x.CompaniesId == firmaId && x.MenuNamesId == menusId);
        }

        public List<Menus> Getirliste(int Id)
        {
            return _MenusDal.GetAll(x => x.Id == Id);
        }

        public List<Menus> Getirlistefirma(int firmaid)
        {
            return _MenusDal.GetAll(x => x.CompaniesId == firmaid);
        }

        public Menus Getirurun(int productId)
        {
            return _MenusDal.Get(x => x.ProductId == productId);
        }

        public List<Menus> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Menus> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Menus Update(Menus entity)
        {
            return _MenusDal.Update(entity);
        }
    }
}
