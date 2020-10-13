using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ICityService
    {
        List<City> GetAll();
        List<City> GetProductsByOgrenciId(int OgrenciId);
        List<City> GetProductsByOgrenciName(string ogrenciName);
        City Get(int Id);
        City Update(City entity);
        void Delete(City entity);
        City Add(City entity);
    }
}
