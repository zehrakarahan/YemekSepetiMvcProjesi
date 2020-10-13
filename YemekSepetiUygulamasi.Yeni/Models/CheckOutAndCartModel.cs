using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Models
{
    public class CheckOutAndCartModel
    {
        public CheckOutmodel CheckOutmodel { get; set; }
        public List<CartLine> CartLines { get; set; }
        public List<CartOrderModel> CartOrders { get; set; }
    }
}
