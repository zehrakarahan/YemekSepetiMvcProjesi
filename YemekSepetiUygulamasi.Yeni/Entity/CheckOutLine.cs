using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Entity
{
    public class CheckOutLine
    {
        public int CheckOutLineId { get; set; }
        public int CheckOutNumber { get; set; }
        public string UserId { get; set; }
        public string ProductCodes { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
    }
}
