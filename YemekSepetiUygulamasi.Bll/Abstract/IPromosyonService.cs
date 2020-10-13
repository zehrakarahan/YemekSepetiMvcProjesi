using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IPromosyonService
    {
        List<Promosyon> GetAll();
        List<Promosyon> GetProductsByOgrenciId(int OgrenciId);
        List<Promosyon> GetProductsByOgrenciName(string ogrenciName);
        Promosyon Get(int Id);
        List<Promosyon> Getirliste(int firmaId);
        Promosyon Update(Promosyon entity);
        void Delete(Promosyon entity);
        Promosyon Add(Promosyon entity);
    }
}
