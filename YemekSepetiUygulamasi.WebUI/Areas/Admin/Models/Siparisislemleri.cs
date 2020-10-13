using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Areas.Admin.Models
{
    public class Siparisislemleri
    {
        public Order Order { get; set; }

        public List<OrderProduct> OrderProduct { get; set; }

        public double siparistutari { get; set; }

        public double siparisindirimlitutari { get; set; }

        public string odemesekli { get; set; }

        
        public Status Status { get; set; }

        public DiscountCoupon DiscountCoupon { get; set; }

        public Customer Customer { get; set; }

        public CustomersAdress CustomersAdress { get; set; }
    }
}