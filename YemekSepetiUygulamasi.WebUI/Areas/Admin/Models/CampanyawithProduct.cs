using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Areas.Admin.Models
{
    public class CampanyawithProduct
    {
        public CampaignPName CampaignPName { get; set; }

        public string kampanyaresim { get; set; }

        public List<Product> Product { get; set; }

        public List<CampaignProduct> CampaignProduct { get; set; }

        public List<ProductImage> ProductImage { get; set; }

        public List<string> Resimler { get; set; }

        public List<Product> Sectikleri { get; set; }

        public List<int> kacincieleman { get; set; }

        public int sayacdegeri { get; set; }
    }
}