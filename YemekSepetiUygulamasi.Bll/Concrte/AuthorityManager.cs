using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Bll.Abstract;
using YemekSepetiUygulamasi.Dal.Abstract;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Concrte
{
    public class AuthorityManager : IAuthorityService
    {
        private IAuthorityDal _AuthorityDal;
        public AuthorityManager(IAuthorityDal AuthorityDal)
        {
            _AuthorityDal = AuthorityDal;
        }

        public Authority Add(Authority entity)
        {
            return _AuthorityDal.Add(entity);
        }

        public void Delete(Authority entity)
        {
            _AuthorityDal.Delete(entity);
        }

        public Authority Get(int sepetId)
        {
            return _AuthorityDal.Get(x => x.Id == sepetId);
        }

        public List<Authority> GetAll()
        {
            return _AuthorityDal.GetAll();
        }

        public List<Authority> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Authority> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Authority Update(Authority entity)
        {
            return _AuthorityDal.Update(entity);
        }
    }
}
