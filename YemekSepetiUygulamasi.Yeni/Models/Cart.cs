using MarketE_Commerce_Site.Entity;
using MarketE_Commerce_Site.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Models
{
    public class Cart
    {
        private List<CartLine> products = new List<CartLine>();
        public List<CartLine> Products => products;

        public void AddProduct(Products product, int quantity,bool pShippingPrice)
        {
            var prd = products
                .Where(i => i.Product.ProductsId == product.ProductsId)
                .FirstOrDefault();

            if (prd == null)
            {
                products.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity,
                    ShippingPrice=pShippingPrice
                });
            }
            else
            {
                prd.Quantity += quantity;
            }
        }

        public void RemoveProduct(Products product)
        {
            products.RemoveAll(i => i.Product.ProductsId == product.ProductsId);
        }
        public double NetTotalPrice()
        {
            return products.Sum(i => i.Product.NetProductPrice * i.Quantity);
        }
        public void ClearAll()
        {
            products.Clear();
        }
    }
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Products Product { get; set; }
        public int Quantity { get; set; }
        public bool ShippingPrice { get; set; }

    }
}
