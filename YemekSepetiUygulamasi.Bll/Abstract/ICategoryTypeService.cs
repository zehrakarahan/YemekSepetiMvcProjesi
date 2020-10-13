using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ICategoryTypeService
    {
        List<Categorytype> GetAll();
        List<Categorytype> GetProductsByOgrenciId(int OgrenciId);
        List<Categorytype> GetProductsByOgrenciName(string ogrenciName);
        Categorytype Get(int Id);
        Categorytype Update(Categorytype entity);
        void Delete(Categorytype entity);
        Categorytype Add(Categorytype entity);
    }
}
