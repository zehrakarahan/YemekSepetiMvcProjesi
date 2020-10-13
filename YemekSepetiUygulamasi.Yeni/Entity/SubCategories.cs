using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Entity
{
    public class SubCategories
    {
        public int SubCategoriesId { get; set; }
        public string SubCategoriesName { get; set; } 
        public int CategoriesId { get; set; }

    }
}
