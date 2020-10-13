using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MarketE_Commerce_Site.Entity;
using MarketE_Commerce_Site.Infastructure;
using MarketE_Commerce_Site.Models;
using MarketE_Commerce_Site.Repository.Abstract;
using MarketE_Commerce_Site.Repository.Concrete.EntityFrameWork;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MarketE_Commerce_Site.Controllers
{
    public class CartController : Controller
    {
        private IUnitOfWorkRepository uow;
        private EcommerceDbContext context;
        public CartController(IUnitOfWorkRepository _uow, EcommerceDbContext _context)
        {
            uow = _uow;
            context = _context;
        }

        public IActionResult Index()
        {
            return View(GetCart());
        }
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity = 1, bool shippingPrice = false)
        {
            bool hede = shippingPrice;
            var product = uow.Product.GetById(productId);
            if (product != null)
            {
                var cart = GetCart();
                cart.AddProduct(product, quantity, shippingPrice);
                SaveChart(cart);
                TempData["SuccesCart"] = $"{product.ProductName} Added Your Cart Succesfully";
            }
            //return RedirectToAction("Index");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveProduct(int product_id)
        {
            var product = uow.Product.GetById(product_id);
            if (product != null)
            {
                var cart = GetCart();
                cart.RemoveProduct(product);
                SaveChart(cart);
                TempData["SuccesCart"] = $"{product.ProductName} Remove Your Cart Succesfully";
            }
            return RedirectToAction("Index", "Home");
        }

        private void SaveChart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }

        private Cart GetCart()
        {
            return HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
        }

        public IActionResult GoToCart()
        {

            var cart = HttpContext.Session.GetJson<Cart>("Cart")?.Products ?? null;
            double product_totalPrice = 0;

            var ListshowCarts = new List<CartOrderModel>();

            foreach (var item in cart)
            {
                var model = new CartOrderModel();
                model.productImage = item.Product.ProductImage;
                model.productName = item.Product.ProductName;
                model.productCode = item.Product.ProductsId;
                model.unitPrice = item.Product.NetProductPrice;
                if (item.ShippingPrice)
                {
                    model.unitPrice = 0;
                    model.ShippingPrice = item.Product.ShippingPrice;
                    model.subTotal = item.Product.ShippingPrice * item.Quantity;
                }
                else
                {
                    model.ShippingPrice = 0;
                    model.unitPrice = item.Product.NetProductPrice;
                    model.subTotal = item.Product.NetProductPrice * item.Quantity;
                }
                model.quantity = item.Quantity;
                //model.subTotal = item.Product.NetProductPrice * item.Quantity;
                ListshowCarts.Add(model);
                product_totalPrice += model.subTotal;
            }
            ViewBag.totalPriceForCart = product_totalPrice;


            return View(ListshowCarts);
        }

        public IActionResult CheckOut()
        {
            double product_totalprice = 0;
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("AccessDeniedPage", "Account");

            var cart = HttpContext.Session.GetJson<Cart>("Cart")?.Products ?? null;

            var ListshowCarts = new List<CartOrderModel>();

            CheckOutAndCartModel checkoutInformations = new CheckOutAndCartModel();


            foreach (var item in cart)
            {
                var model = new CartOrderModel();
                model.productImage = item.Product.ProductImage;
                model.productName = item.Product.ProductName;
                model.productCode = item.Product.ProductsId;
                model.unitPrice = item.Product.NetProductPrice;
                if (item.ShippingPrice)
                {
                    model.unitPrice = 0;
                    model.ShippingPrice = item.Product.ShippingPrice;
                    model.subTotal = item.Product.ShippingPrice * item.Quantity;
                }
                else
                {
                    model.ShippingPrice = 0;
                    model.unitPrice = item.Product.NetProductPrice;
                    model.subTotal = item.Product.NetProductPrice * item.Quantity;
                }
                model.quantity = item.Quantity;
                ListshowCarts.Add(model);
                ViewBag.totalPriceForCart = product_totalprice;
                checkoutInformations.CartOrders = ListshowCarts;
            }


            return View(checkoutInformations);
        }
        [HttpPost]
        public IActionResult CheckOut(CheckOutAndCartModel model)
        {
            // try catch blguna al burayı  UNUTMA

            var cart = HttpContext.Session.GetJson<Cart>("Cart")?.Products ?? null;

            //    Dictionary<string, dynamic> responseData = new Dictionary<string, dynamic>();
            //    string data = "entityId=8a8294174b7ecb28014b9699220015ca" +
            //"&amount=92.00" +
            //"&currency=EUR" +
            //"&paymentType=DB";

            //bool res = GetCartResult(responseData, data);


            List<CartLine> cartline = new List<CartLine>();
            CheckOutAndCartModel checkoutInformations = new CheckOutAndCartModel();
            Random random = new Random();
            int guid = random.Next(100, 9999);


            //CheckoutModel Gereksiz sil
            var UserCartSaveInformation = new CheckOutLine();
            var userGeneralInformation = new CheckOut();

            foreach (var item in cart)
            {
                cartline.Add(new CartLine
                {
                    CartLineId = guid,
                    Product = item.Product,
                    Quantity = item.Quantity,
                    ShippingPrice = item.ShippingPrice
                });
                checkoutInformations.CartLines = cartline;
            }
            foreach (var item2 in checkoutInformations.CartLines)
            {
                UserCartSaveInformation.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                UserCartSaveInformation.CheckOutNumber = guid;
                UserCartSaveInformation.ProductCodes += item2.Product.ProductsId + " ) " + item2.Product.ProductName + " , ";
                UserCartSaveInformation.CheckOutDate = DateTime.Now;
                UserCartSaveInformation.Quantity = item2.Quantity;
                UserCartSaveInformation.Total = 55;
            }
            uow.CheckOutLine.Add(UserCartSaveInformation);
            uow.SaveChanges();


            userGeneralInformation.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userGeneralInformation.CheckOutLineId = guid;
            userGeneralInformation.FirstName = model.CheckOutmodel.FirstName;
            userGeneralInformation.LastName = model.CheckOutmodel.LastName;
            userGeneralInformation.Email = model.CheckOutmodel.Email;
            userGeneralInformation.Telephone = model.CheckOutmodel.Telephone;
            userGeneralInformation.Address1 = model.CheckOutmodel.Address1;
            userGeneralInformation.Address2 = model.CheckOutmodel.Address2;
            userGeneralInformation.City = model.CheckOutmodel.City;
            userGeneralInformation.PostCode = model.CheckOutmodel.PostCode;
            userGeneralInformation.PaymentMethod = model.CheckOutmodel.PaymentMethod;
            userGeneralInformation.Total = model.CheckOutmodel.Total;
            userGeneralInformation.IsApprovePayment = model.CheckOutmodel.IsApprovePayment; // <= odeme onaylanırsa True yada false donecek servisden gelen degere gore
            userGeneralInformation.Result = "Hede"; // servisten donen strin basılcak
            uow.CheckOut.Add(userGeneralInformation);
            uow.SaveChanges();

            return View();
        }

        private bool GetCartResult(Dictionary<string, dynamic> pResponeseData, string pData)
        {
            pResponeseData = new Dictionary<string, dynamic>();
            string url = "https://test.vr-pay-ecommerce.de/v1/checkouts";
            byte[] buffer = Encoding.ASCII.GetBytes(pData);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.Headers["Authorization"] = "Bearer OGE4Mjk0MTc0ZTczNWQwYzAxNGU3OGJlYjZjNTE1NGZ8Y1RaakFtOWM4Nw=";
            request.ContentType = "application/x-www-form-urlencoded";
            Stream PostData = request.GetRequestStream();
            PostData.Write(buffer, 0, buffer.Length);
            PostData.Close();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                //var s = new JavaScriptSerializer();
                //pResponeseData = s.Deserialize<Dictionary<string, dynamic>>(reader.ReadToEnd());
                pResponeseData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(reader.ReadToEnd());
                reader.Close();
                dataStream.Close();
            }
            //pResponeseData = Request()["result"]["description"];
            return true;

        }
    }
}
