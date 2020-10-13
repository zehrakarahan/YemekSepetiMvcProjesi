using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ICampaignPNameService
    {
        List<CampaignPName> GetAll();
        List<CampaignPName> GetProductsByOgrenciId(int OgrenciId);
        List<CampaignPName> GetProductsByOgrenciName(string ogrenciName);
        CampaignPName Get(int Id);
        CampaignPName Update(CampaignPName entity);
        void Delete(CampaignPName entity);
        CampaignPName Add(CampaignPName entity);
        CampaignPName GetirBilgi(int firmaId, int Id);
        List<CampaignPName> Getirliste(int firmaId);
    }
}
