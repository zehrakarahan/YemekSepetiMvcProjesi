using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Areas.Admin.Models
{
    public class KampanyaBilgilerimodel
    {
        public CampaignPName CampaignPName { get; set; }

        public int sayac { get; set; } = 1;

        public List<Product> Productliste { get; set; }

        public List<CampaignProduct> CampaignProductliste { get; set; }

        public HttpPostedFileBase kampanyaresim { get; set; }

        public string fiyat { get; set; }


        public List<Product> Productsectikleri { get; set; }
    }
}