using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MarketE_Commerce_Site.Models;
using MarketE_Commerce_Site.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using static MarketE_Commerce_Site.Models.PagingModel;

namespace MarketE_Commerce_Site.Controllers
{

    public class HomeController : Controller
    {
        //public int PageSize = 8;
        public IUnitOfWorkRepository uow;
        public HomeController(IUnitOfWorkRepository _uow)
        {
            uow = _uow;
        }

        public IActionResult Index()
        {
            var getPropertiesForMainPage = uow.MainPageSetup.GetAll().FirstOrDefault();

            if (getPropertiesForMainPage != null)
            {
                var MainpagesetupMode = new MainPageSetupsModel();
                MainpagesetupMode.NewsIsActive = getPropertiesForMainPage.NewsIsActive;
                MainpagesetupMode.NewsHeader = getPropertiesForMainPage.NewsHeader;
                MainpagesetupMode.NewsContext = getPropertiesForMainPage.NewsContext;

                return View(MainpagesetupMode);

            }
            else
            {
                return View();
            }
        }
        public IActionResult Product(int product_id)
        {

            var get_Product = uow.Product.GetById(product_id);

            return View(get_Product);
        }
        public IActionResult ProductList(int id, int page = 1, int PageSize = 8, int categoryid =0)
        {
            if (id == 0)
            {
                var products = uow.Product.GetAll();
                var count = products.Count();
                products = products.Skip((page - 1) * PageSize).Take(PageSize);
                return View(
                    new ProducListModel()
                    {
                        products = products,
                        pagingInfo = new PagingModel
                        {
                            CurrentPage = page,
                            ItemsPerPage = PageSize,
                            TotalItems = count

                        }
                    });
            }
            else
            {
                var products = uow.Product.Find(x => x.CategoryId == id);
                var count = products.Count();
                products = products.Skip((page - 1) * PageSize).Take(PageSize);
                return View(
                      new ProducListModel()
                      {
                          products = products,
                          pagingInfo = new PagingModel
                          {
                              CurrentPage = page,
                              ItemsPerPage = PageSize,
                              TotalItems = count

                          }
                      });
            }
        }

        public IActionResult GetToCart()
        {
            return View();
        }
        public IActionResult AddToCart(int ProductId)
        {
            return View();
        }
    }
}
