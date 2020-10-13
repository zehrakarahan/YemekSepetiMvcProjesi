using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Models
{
    public class CartOrderModel
    {
        public string productName { get; set; }
        public string productImage { get; set; }
        public int productCode { get; set; }
        public double unitPrice { get; set; }
        public double ShippingPrice { get; set; }
        public int quantity { get; set; }
        public double subTotal { get; set; }
        public double totalPrice { get; set; }
    }
}
