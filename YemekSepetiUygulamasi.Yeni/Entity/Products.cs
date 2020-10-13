using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Entity
{
    public class Products
    {
        public int ProductsId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        public string ProductSmallDescription { get; set; }

        public double BrutProductPrice { get; set; }
        public double NetProductPrice { get; set; }
        public double ShippingPrice { get; set; }

        public int ProductQuantity { get; set; }

        public string ProductImage { get; set; }

        public bool IsApproved { get; set; }
        public bool IsFeatured { get; set; }
        public bool Bestsellers { get; set; }
        public bool IsNew { get; set; }

        public DateTime DateAdded { get; set; }
        public int CategoryId { get; set; }
        //gerek yok gibi silebiliriz
        public Categories categories { get; set; }
        public int SubCategoryId { get; set; }
        //gerek yok gibi silebiliriz
        public SubCategories SubCategories { get; set; }
        public bool productIsActive { get; set; }
        public string ShippingDescription { get; set; }
        public string cancellationProcedure { get; set; }

    }
}
