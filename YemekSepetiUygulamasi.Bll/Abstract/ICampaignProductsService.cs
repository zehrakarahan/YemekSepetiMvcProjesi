using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ICampaignProductsService
    {
        List<CampaignProduct> GetAll();
        List<CampaignProduct> GetProductsByOgrenciId(int OgrenciId);
        List<CampaignProduct> GetProductsByOgrenciName(string ogrenciName);
        CampaignProduct Get(int Id);
        List<CampaignProduct> Getir(int firmaId, int campaignameId);
        CampaignProduct Update(CampaignProduct entity);
        void Delete(CampaignProduct entity);
        CampaignProduct Add(CampaignProduct entity);

    }
}
