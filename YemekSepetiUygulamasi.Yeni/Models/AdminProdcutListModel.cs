using MarketE_Commerce_Site.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Models
{
    public class AdminProdcutListModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductSmallDescription { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
     

    }
}
