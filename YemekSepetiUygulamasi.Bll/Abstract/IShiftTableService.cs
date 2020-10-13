using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IShiftTableService
    {
        List<ShiftTable> GetAll();
        List<ShiftTable> GetProductsByOgrenciId(int OgrenciId);
        List<ShiftTable> GetProductsByOgrenciName(string ogrenciName);
        ShiftTable Get(int Id);
        ShiftTable Update(ShiftTable entity);
        void Delete(ShiftTable entity);
        ShiftTable Add(ShiftTable entity);
    }
}
