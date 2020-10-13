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
    public class FavoritesManager : IFavoritesService
    {
        private IFavoritesDal _FavoritesDal;
        public FavoritesManager(IFavoritesDal FavoritesDal)
        {
            _FavoritesDal = FavoritesDal;
        }
        public Favorites Add(Favorites entity)
        {
            return _FavoritesDal.Add(entity);
        }

        public void Delete(Favorites entity)
        {
            _FavoritesDal.Delete(entity);
        }

        public Favorites Get(int Id)
        {
            return _FavoritesDal.Get(x => x.Id == Id);
        }

        public List<Favorites> GetAll()
        {
            return _FavoritesDal.GetAll();
        }

        public List<Favorites> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Favorites> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Favorites Update(Favorites entity)
        {
            return _FavoritesDal.Update(entity);
        }
    }
}
