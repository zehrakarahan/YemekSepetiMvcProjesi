using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ISalesService
    {
        List<Sales> GetAll();
        List<Sales> GetProductsByOgrenciId(int OgrenciId);
        List<Sales> GetProductsByOgrenciName(string ogrenciName);
        Sales Get(int Id);
        Sales Update(Sales entity);
        void Delete(Sales entity);
        Sales Add(Sales entity);
    }
}
