using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ICustomerService
    {
        List<Customer> GetAll();
        List<Customer> GetProductsByOgrenciId(int OgrenciId);
        List<Customer> GetProductsByOgrenciName(string ogrenciName);
        Customer KullaniciGiris(string kullaniciadi, string parola);
        Customer  GetKullaniciKullaniciName(string Kullaniciadi);
        Customer EmailDogrula(string Email);
        Customer Get(int Id);
        Customer Update(Customer entity);
        void Delete(Customer entity);
        Customer Add(Customer entity);
    }
}
