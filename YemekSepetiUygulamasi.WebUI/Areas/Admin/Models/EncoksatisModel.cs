using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Areas.Admin.Models
{
    public class EncoksatisModel
    {
       public List<int> ProductId { get; set; }

       public List<Product> Productlist { get; set; }

       public List<string> productresmilist { get; set; }

       public List<int> MenuNameId { get; set; }

       public List<MenuNames> MenuNameslist { get; set; }

       public List<string> menuresmilist { get; set; }

       public List<int> KampanyaId { get; set; }

        public List<CampaignPName> CampaignPNamelist { get; set; }

        public List<string> kampanyaresmi { get; set; }

       public List<int> ProductSayisi { get; set; }

        public List<int> MenuNameSayisi { get; set; }

        public List<int> KampanyaSayisi { get; set; }

    }
}