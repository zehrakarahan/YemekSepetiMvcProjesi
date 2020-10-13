using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Models
{
    public class Searchler
    {
        public List<Category> CategoryListe { get; set; }

        public List<Product> ProductListe { get; set; }

        public List<Companies> FirmaListesi { get; set; }
    }
}