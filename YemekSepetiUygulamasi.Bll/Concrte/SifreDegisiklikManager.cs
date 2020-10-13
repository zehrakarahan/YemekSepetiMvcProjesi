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
    public class SifreDegisiklikManager:ISifreDegisiklikService
    {
        private ISifreDegisiklikDal _SifreDegisiklikDal;
        public SifreDegisiklikManager(ISifreDegisiklikDal SifreDegisiklikDal)
        {
            _SifreDegisiklikDal = SifreDegisiklikDal;
        }

        public SifreDegisiklik Add(SifreDegisiklik entity)
        {
            return _SifreDegisiklikDal.Add(entity);
        }

        public void Delete(SifreDegisiklik entity)
        {
            _SifreDegisiklikDal.Delete(entity);
        }

        public SifreDegisiklik Get(string guid)
        {
            return _SifreDegisiklikDal.Get(x => x.guidimiz == guid);
        }

        public SifreDegisiklik Get(int Id)
        {
            return _SifreDegisiklikDal.Get(x => x.Id == Id);
        }

        public List<SifreDegisiklik> GetAll()
        {
            return _SifreDegisiklikDal.GetAll();
        }

        public List<SifreDegisiklik> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<SifreDegisiklik> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public SifreDegisiklik Update(SifreDegisiklik entity)
        {
            return _SifreDegisiklikDal.Update(entity);
        }
    }
}
