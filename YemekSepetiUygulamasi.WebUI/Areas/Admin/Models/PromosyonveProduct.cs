using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Areas.Admin.Models
{
    public class PromosyonveProduct
    {
        public List<Promosyon> Promosyonlist{ get; set; }

        public string promosyonorani { get; set; }

        public List<Product> Productlist { get; set; }

        public List<string> UrunResmi { get; set; }

        public Product Product { get; set; }

        public Promosyon Promosyon { get; set; }
    }
}