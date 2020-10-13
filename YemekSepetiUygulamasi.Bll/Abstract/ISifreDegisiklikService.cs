using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ISifreDegisiklikService
    {
        List<SifreDegisiklik> GetAll();
        List<SifreDegisiklik> GetProductsByOgrenciId(int OgrenciId);
        List<SifreDegisiklik> GetProductsByOgrenciName(string ogrenciName);
        SifreDegisiklik Get(string guid);
        SifreDegisiklik Update(SifreDegisiklik entity);
        void Delete(SifreDegisiklik entity);
        SifreDegisiklik Add(SifreDegisiklik entity);
    }
}
