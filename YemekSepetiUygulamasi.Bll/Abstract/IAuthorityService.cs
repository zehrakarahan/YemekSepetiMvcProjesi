using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IAuthorityService
    {
        List<Authority> GetAll();
        List<Authority> GetProductsByOgrenciId(int OgrenciId);
        List<Authority> GetProductsByOgrenciName(string ogrenciName);
        Authority Get(int sepetId);
        Authority Update(Authority entity);
        void Delete(Authority entity);
        Authority Add(Authority entity);
    }
}
