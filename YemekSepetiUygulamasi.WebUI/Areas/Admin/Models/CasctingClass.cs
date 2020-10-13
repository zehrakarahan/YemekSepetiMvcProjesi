using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Areas.Admin.Models
{
    public class CasctingClass
    {

        public Companies Companies { get; set; }

        public int CityId { get; set; }

        public int CountyId { get; set; }

        public int NeighborhoodId { get; set; }
    }
}