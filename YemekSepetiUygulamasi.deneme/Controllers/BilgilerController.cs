using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YemekSepetiUygulamasi.deneme.Controllers
{
    public class BilgilerController : Controller
    {
        // GET: Bilgiler
        public ActionResult Index()
        {
         
             var email=TempData["email"];
            var name = TempData["first_name"];
            var lastname = TempData["lastname"];
            TempData["emailad"] = email;
            TempData["adi"] = name;
            TempData["soyadi"] = lastname;
    
            return View();
        }
    }
}