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
    public class CampaignProductsManager : ICampaignProductsService
    {
        private ICampaignProductsDal _campaignproductsDal;
        public CampaignProductsManager(ICampaignProductsDal campaignproductsDal)
        {
            _campaignproductsDal = campaignproductsDal;
        }

        public CampaignProduct Add(CampaignProduct entity)
        {
            return _campaignproductsDal.Add(entity);
        }

        public void Delete(CampaignProduct entity)
        {
            _campaignproductsDal.Delete(entity);
        }

        public CampaignProduct Get(int Id)
        {
            return _campaignproductsDal.Get(x => x.Id == Id);
        }

        public List<CampaignProduct> GetAll()
        {
            return _campaignproductsDal.GetAll();
        }

        public List<CampaignProduct> Getir(int firmaId, int campaignameId)
        {
            return _campaignproductsDal.GetAll(x => x.CompaniesId == firmaId && x.CampaignPNameId == campaignameId);

        }

        public List<CampaignProduct> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<CampaignProduct> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public CampaignProduct Update(CampaignProduct entity)
        {
            return _campaignproductsDal.Update(entity);
        }
    }
}
