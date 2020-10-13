using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Models
{
    public class CategoryBilgileri
    {
        public Category Category { get; set; }

        public HttpPostedFileBase resimbilgi { get; set; }
    }
}