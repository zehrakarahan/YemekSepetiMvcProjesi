using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ICountryService
    {
        List<Country> GetAll();
        List<Country> GetProductsByOgrenciId(int OgrenciId);
        List<Country> GetProductsByOgrenciName(string ogrenciName);
        Country Get(int sepetId);
        Country Update(Country entity);
        void Delete(Country entity);
        Country Add(Country entity);
    }
}
