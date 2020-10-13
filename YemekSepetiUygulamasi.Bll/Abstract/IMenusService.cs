using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IMenusService
    {
        List<Menus> GetAll();
        List<Menus> GetProductsByOgrenciId(int OgrenciId);
        List<Menus> GetProductsByOgrenciName(string ogrenciName);
        Menus Get(int Id);
        Menus Get(int firmaId, int menunamesId);
        Menus Getirurun(int productId);
        List<Menus> Getir(int firmaId,int menusId);
        Menus Update(Menus entity);
        void Delete(Menus entity);
        Menus Add(Menus entity);
        List<Menus> Getirliste(int Id);
        List<Menus> Getirlistefirma(int firmaid);
    }
}
