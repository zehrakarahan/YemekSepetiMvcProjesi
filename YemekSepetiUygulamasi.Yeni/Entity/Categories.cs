using MarketE_Commerce_Site.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Entity
{
    public class Categories
    {
        public int CategoriesId { get; set; }
        [Required]
        [Display(Name ="Category Name")]
        public string CategoriesName { get; set; }

        

    }
}
