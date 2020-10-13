using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Models
{
    public class UserAndCartInformationModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProductsName { get; set; }
        public double Total { get; set; }
        public int CardId { get; set; }
        public DateTime CheckOutDate { get; set; }

    }
}
