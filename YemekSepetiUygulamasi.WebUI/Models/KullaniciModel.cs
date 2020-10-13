using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Models
{
    public class KullaniciModel
    {
        public Customer Customer { get; set; }
        public string Yenisifre { get; set; }
        public string Şifretekrar { get; set; }
    }
}