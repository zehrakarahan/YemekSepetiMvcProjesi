using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Areas.Admin.Models
{
    public class Siparislerim
    {
        public Order  Order { get; set; }

        public List<OrderProduct> OrderProduct { get; set; }


        public List<MenuNames> MenuNames { get; set; }

        public List<CampaignPName> CampaignProductName { get; set; }

        public List<Product> Product { get; set; }

        public Customer Customer { get; set; }

        public CustomersAdress CustomersAdress { get; set; }

        public Status Status { get; set; }

        public DiscountCoupon DiscountCoupon { get; set; }
    }
    
}