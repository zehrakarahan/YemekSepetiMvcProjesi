using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IManagerTableService
    {
        List<ManagerTable> GetAll();
        List<ManagerTable> GetProductsByOgrenciId(int OgrenciId);
        List<ManagerTable> GetProductsByOgrenciName(string ogrenciName);
        ManagerTable Get(string Kullanici,string sifre);
        ManagerTable Geti(string Kullanici);
        ManagerTable Get(int sepetId);
        ManagerTable Getir(string emailadresi);
        ManagerTable Update(ManagerTable entity);
        void Delete(ManagerTable entity);
        ManagerTable Add(ManagerTable entity);
    }
}
