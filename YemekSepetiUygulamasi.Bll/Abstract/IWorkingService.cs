using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IWorkingService
    {
        List<Working> GetAll();
        List<Working> GetProductsByOgrenciId(int OgrenciId);
        List<Working> GetProductsByOgrenciName(string ogrenciName);
        Working Get(int Id);
        Working Update(Working entity);
        void Delete(Working entity);
        Working Add(Working entity);
    }
}
