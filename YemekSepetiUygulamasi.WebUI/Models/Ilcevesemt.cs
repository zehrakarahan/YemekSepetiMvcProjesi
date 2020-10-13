using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Models
{
    public class Ilcevesemt
    {
        public string secilensemt { get; set; }

       public List<string> semtveilceid { get; set; }

        public List<string> semtveilcebilgi { get; set; }

        
    }
}