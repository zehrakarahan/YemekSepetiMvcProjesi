using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ISandDtableService
    {
        List<SandDtable> GetAll();
        List<SandDtable> GetProductsByOgrenciId(int OgrenciId);
        List<SandDtable> GetProductsByOgrenciName(string ogrenciName);
        SandDtable Get(int Id);
        SandDtable Update(SandDtable entity);
        void Delete(SandDtable entity);
        SandDtable Add(SandDtable entity);
    }
}
