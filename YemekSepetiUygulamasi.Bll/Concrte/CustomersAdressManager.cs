using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;
using YemekSepetiUygulamasi.Bll.Abstract;
using YemekSepetiUygulamasi.Dal.Abstract;

namespace YemekSepetiUygulamasi.Bll.Concrte
{
    public class CustomersAdressManager : ICustomersAdressService
    {
        private ICustomersAdressDal _CustomersAdressDal;
        public CustomersAdressManager(ICustomersAdressDal CustomersAdressDal)
        {
            _CustomersAdressDal = CustomersAdressDal;
        }

        public CustomersAdress Add(CustomersAdress entity)
        {
            return _CustomersAdressDal.Add(entity);
        }

        public void Delete(CustomersAdress entity)
        {
            _CustomersAdressDal.Delete(entity);
        }

        public CustomersAdress Get(int Id)
        {
            return _CustomersAdressDal.Get(x => x.Id == Id);
        }

        public List<CustomersAdress> GetAll()
        {
            return _CustomersAdressDal.GetAll(); 
        }

        public CustomersAdress getir(int firmaId, int id)
        {
            return _CustomersAdressDal.Get(x => x.CompaniesId == firmaId && x.Id == id);
        }

        public List<CustomersAdress> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<CustomersAdress> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public CustomersAdress Update(CustomersAdress entity)
        {
            return _CustomersAdressDal.Update(entity);
        }
    }
}
