using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Areas.Admin.Models
{
    public class AdminKullaniciSifre
    {
        public ManagerTable ManagerTable { get; set; }
        public string yenisifre { get; set; }
        public string sifretekrar { get; set; }
    }
}