using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Models
{
    public class CheckOutmodel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
        public string PaymentMethod { get; set; }
        public double Total { get; set; }
        public bool IsApprovePayment { get; set; }
        public string Result { get; set; }
    }
}
