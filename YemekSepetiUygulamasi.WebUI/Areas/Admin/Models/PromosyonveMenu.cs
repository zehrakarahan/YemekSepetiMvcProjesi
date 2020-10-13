using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Areas.Admin.Models
{
    public class PromosyonveMenu
    {
        public List<Promosyon> Promosyonlist { get; set; }

        public List<MenuNames> MenuNameslist { get; set; }


        public MenuNames MenuNames { get; set; }

        public Promosyon Promosyon { get; set; }
    }
}