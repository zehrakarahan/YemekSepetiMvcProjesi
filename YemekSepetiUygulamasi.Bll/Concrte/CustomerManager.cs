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
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _CustomerDal;
        public CustomerManager(ICustomerDal CustomerDal)
        {
            _CustomerDal = CustomerDal;
        }

        public Customer Add(Customer entity)
        {
            return _CustomerDal.Add(entity);
        }

        public void Delete(Customer entity)
        {
            _CustomerDal.Delete(entity);
        }

        public Customer Get(int Id)
        {
            return _CustomerDal.Get(x => x.Id == Id);
        }

        public List<Customer> GetAll()
        {
            return _CustomerDal.GetAll();
        }
        YemekSeyetContextt db = new YemekSeyetContextt();
        public List<Customer> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Customer KullaniciGiris(string kullaniciadi, string parola)
        {
            try
            {
              
                var sifre = new ToPasswordRepository().Md5(parola);
                var kullanici = db.Customer.Where(x => x.Cname ==kullaniciadi && x.Password == sifre).FirstOrDefault();
                var kullaniciemail = db.Customer.Where(x => x.EmailAdress == kullaniciadi && x.Password == sifre).FirstOrDefault();
                if(kullaniciemail!=null)
                {
                    return kullaniciemail;
                }
                if(kullanici!=null)
                {
                    return kullanici;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception error)
            {

                throw new Exception("Kullanici Giriş Hata:" + error.Message);
            }
        }

        public Customer Update(Customer entity)
        {
            return _CustomerDal.Update(entity);
        }

        public Customer EmailDogrula(string Email)
        {
            var kisi=_CustomerDal.Get(x => x.EmailAdress == Email);
            if(kisi!=null)
            {
                return kisi;
            }
            else
            {
                return null;
            }
        }

        public Customer GetKullaniciKullaniciName(string Kullaniciadi)
        {
            return _CustomerDal.Get(x => x.UserName == Kullaniciadi);
        }
    }
}
