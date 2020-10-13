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
    public class PromosyonManager : IPromosyonService
    {
        private IPromosyonDal _promosyonDal;
        public PromosyonManager(IPromosyonDal promosyonDal)
        {
           this._promosyonDal = promosyonDal;
        }

        public Promosyon Add(Promosyon entity)
        {
            return _promosyonDal.Add(entity);
        }

        public void Delete(Promosyon entity)
        {
             _promosyonDal.Delete(entity);
        }

        public Promosyon Get(int Id)
        {
            return _promosyonDal.Get(x => x.Id == Id);
        }

        public List<Promosyon> GetAll()
        {
            return _promosyonDal.GetAll();
        }

        public List<Promosyon> Getirliste(int firmaId)
        {
            return _promosyonDal.GetAll(x => x.CompaniesId == firmaId);
        }

        public List<Promosyon> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Promosyon> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Promosyon Update(Promosyon entity)
        {
            return _promosyonDal.Update(entity);
        }
    }
}
