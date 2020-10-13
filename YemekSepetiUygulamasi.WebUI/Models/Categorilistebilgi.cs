using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Models
{
    public class Categorilistebilgi
    {
        public List<Category> Categoryliste { get; set; }

        public List<string> resimbilgilistesi { get; set; }
    }
}