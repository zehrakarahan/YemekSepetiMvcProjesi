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
    public class CompaniesManager : ICompaniesService
    {
        private ICompaniesDal _CompaniesDal;
        public CompaniesManager(ICompaniesDal CompaniesDal)
        {
            _CompaniesDal = CompaniesDal;
        }

        public Companies Add(Companies entity)
        {
            return _CompaniesDal.Add(entity);
        }

        public void Delete(Companies entity)
        {
            _CompaniesDal.Delete(entity);  
        }

        public Companies Get(string firmaadi)
        {
            return _CompaniesDal.Get(x => x.CompanyName == firmaadi);
        }

        public Companies Get(int Id)
        {
            return _CompaniesDal.Get(x => x.Id == Id);
        }

        public List<Companies> GetAll()
        {
            return _CompaniesDal.GetAll();
        }

        public List<Companies> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Companies> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Companies Update(Companies entity)
        {
            return _CompaniesDal.Update(entity);
        }
    }
}
