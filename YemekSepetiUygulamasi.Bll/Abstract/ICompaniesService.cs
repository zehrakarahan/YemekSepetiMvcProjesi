using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ICompaniesService
    {
        List<Companies> GetAll();
        List<Companies> GetProductsByOgrenciId(int OgrenciId);
        List<Companies> GetProductsByOgrenciName(string ogrenciName);
        Companies Get(int Id);
        Companies Get(string firmaadi);
        Companies Update(Companies entity);
        void Delete(Companies entity);
        Companies Add(Companies entity);
    }
}
