using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ICustomersAdressService
    {
        List<CustomersAdress> GetAll();
        List<CustomersAdress> GetProductsByOgrenciId(int OgrenciId);
        List<CustomersAdress> GetProductsByOgrenciName(string ogrenciName);
        CustomersAdress Get(int Id);
        CustomersAdress Update(CustomersAdress entity);
        void Delete(CustomersAdress entity);
        CustomersAdress Add(CustomersAdress entity);
        CustomersAdress getir(int firmaId, int id);
    }
}
