using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Models
{
    public class Firmamenuveurunlistesi
    {
        public List<Companies> Companies { get; set; }

        public List<MenuNames> MenuNames { get; set; }

        public List<Category> Category { get; set; }
        
        public List<Product> Product { get; set; }
        
        public List<string> Resimler { get; set; }
    }
}