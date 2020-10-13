using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ICountyService
    {
        List<County> GetAll();
        List<County> GetProductsByOgrenciId(int OgrenciId);
        List<County> GetProductsByOgrenciName(string ogrenciName);
        County Get(int Id);
        List<County> Getiril(int ilId);
        List<County> Getir(int CityId);
        County Update(County entity);
        void Delete(County entity);
        County Add(County entity);
    }
}
