using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IDayTableService
    {
        List<DayTable> GetAll();
        List<DayTable> GetProductsByOgrenciId(int OgrenciId);
        List<DayTable> GetProductsByOgrenciName(string ogrenciName);
        DayTable Get(int Id);
        DayTable Update(DayTable entity);
        void Delete(DayTable entity);
        DayTable Add(DayTable entity);
    }
}
