using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Areas.Admin.Models
{
    public class PromosyonveKampanya
    {
        public List<Promosyon> Promosyonlist { get; set; }

        public List<CampaignPName> CampaignPNamelist { get; set; }


        public CampaignPName CampaignPName { get; set; }

        public Promosyon Promosyon { get; set; }
    }
}