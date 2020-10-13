using MarketE_Commerce_Site.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Models
{
    public class PagingModel
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPage()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
        public class ProducListModel
        {
            public IEnumerable<Products> products { get; set; }
            public PagingModel pagingInfo { get; set; }
        }
    }
}
