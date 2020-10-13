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
    public class CampaignPNameManager : ICampaignPNameService
    {
        private ICampaignPNameDal _CampaignProductNameDal;
        public CampaignPNameManager(ICampaignPNameDal CampaignProductNameDal)
        {
            _CampaignProductNameDal = CampaignProductNameDal;
        }

        public CampaignPName Add(CampaignPName entity)
        {
            return _CampaignProductNameDal.Add(entity);
        }

        public void Delete(CampaignPName entity)
        {
            _CampaignProductNameDal.Delete(entity);
        }

        public CampaignPName Get(int Id)
        {
            return _CampaignProductNameDal.Get2(x => x.Id == Id);
        }

        public List<CampaignPName> GetAll()
        {
            return _CampaignProductNameDal.GetAll();
        }

        public CampaignPName GetirBilgi(int firmaId, int Id)
        {
            return _CampaignProductNameDal.Get(x => x.CompaniesId == firmaId && x.Id == Id);
        }

        public List<CampaignPName> Getirliste(int firmaId)
        {
            return _CampaignProductNameDal.GetAll(x=>x.CompaniesId==firmaId);
        }

        public List<CampaignPName> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<CampaignPName> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public CampaignPName Update(CampaignPName entity)
        {
            return _CampaignProductNameDal.Update(entity);
        }
    }
}
