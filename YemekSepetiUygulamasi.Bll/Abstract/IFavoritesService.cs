using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IFavoritesService
    {
        List<Favorites> GetAll();
        List<Favorites> GetProductsByOgrenciId(int OgrenciId);
        List<Favorites> GetProductsByOgrenciName(string ogrenciName);
        Favorites Get(int Id);
        Favorites Update(Favorites entity);
        void Delete(Favorites entity);
        Favorites Add(Favorites entity);
    }
}
