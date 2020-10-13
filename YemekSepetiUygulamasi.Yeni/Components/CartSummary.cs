using MarketE_Commerce_Site.Infastructure;
using MarketE_Commerce_Site.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Components
{
    public class CartSummary : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var SessionCartLine = HttpContext.Session.GetJson<Cart>("Cart")?.Products ?? null;
            ViewBag.CartLine = SessionCartLine;
            if (SessionCartLine != null)
            {
                if (SessionCartLine.Count() > 1)
                {
                    double result = 0;
                    foreach (var item in SessionCartLine)
                    {
                        if (item.ShippingPrice)
                        {
                            result += item.Product.ShippingPrice * item.Quantity;
                        }
                        else
                        {
                            result += item.Product.NetProductPrice * item.Quantity;
                        }
                    }
                    ViewBag.ItemTotalPrice = result;

                }
                else
                {
                    if (SessionCartLine.Select(x => x.ShippingPrice == false).FirstOrDefault())
                    {
                        ViewBag.ItemTotalPrice = SessionCartLine.Select(i => i.Product.NetProductPrice * i.Quantity).FirstOrDefault();
                    }
                    else
                    {
                        ViewBag.ItemTotalPrice = SessionCartLine.Select(i => i.Product.ShippingPrice * i.Quantity).FirstOrDefault();
                    }
                }
            }


            return View();
            //return HttpContext.Session.GetJson<Cart>("Cart")?.Products.Count().ToString() ?? "0";
        }
    }
}
