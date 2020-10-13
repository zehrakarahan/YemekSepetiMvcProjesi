using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface INeighborhoodService
    {
        List<Neighborhood> GetAll();
        List<Neighborhood> GetProductsByOgrenciId(int OgrenciId);
        List<Neighborhood> GetProductsByOgrenciName(string ogrenciName);
        Neighborhood Get(int Id);
        
        List<Neighborhood> Getir(int countyId);
        Neighborhood Update(Neighborhood entity);
        void Delete(Neighborhood entity);
        Neighborhood Add(Neighborhood entity);
    }
}
