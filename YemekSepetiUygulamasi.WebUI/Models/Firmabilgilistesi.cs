using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Models
{
    public class Firmabilgilistesi
    {
        public List<MenuNames> Menunamelistesi { get; set; }

        public List<Menus> Menulistesi { get; set; }

        public Companies firmabilgileri { get; set; }

        public List<Comment> yorumlistesi { get; set; }

    }
}