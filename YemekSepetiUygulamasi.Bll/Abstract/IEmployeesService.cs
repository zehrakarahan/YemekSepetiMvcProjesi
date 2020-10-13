using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IEmployeesService
    {
        List<Employess> GetAll();
        List<Employess> GetProductsByOgrenciId(int OgrenciId);
        List<Employess> GetProductsByOgrenciName(string ogrenciName);
        Employess Get(int Id);
        Employess Update(Employess entity);
        void Delete(Employess entity);
        Employess Add(Employess entity);
    }
}
