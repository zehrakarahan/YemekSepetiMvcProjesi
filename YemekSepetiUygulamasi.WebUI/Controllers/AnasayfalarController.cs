using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YemekSepetiUygulamasi.Bll;
using YemekSepetiUygulamasi.Bll.Abstract;
using YemekSepetiUygulamasi.Entity.EntityTable;
using YemekSepetiUygulamasi.WebUI.Models;
using YemekSepetiUygulamasi.WebUI.ServiceRedis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Xml;

namespace YemekSepetiUygulamasi.WebUI.Controllers
{
    public class AnasayfalarController : Controller
    {

        // GET: Anasayfalar
        ICityService _cityservice;
        ISliderService _sliderService;
        ICountyService _countyservice;
        ICompaniesService _companieservice;
        INeighborhoodService _neighborhoodservice;
        IProductService _productservice;
        ICategoryService _categoryservice;
        IMenuNamesService _menunameservice;
        private const string CacheName = "MyFirstCacheVariableInRedis";
        private readonly ICacheProvider _cacheProvider;
        public AnasayfalarController(IMenuNamesService menunameservice, ICategoryService categoryservice,IProductService productservice,INeighborhoodService neighborhoodservice, ICompaniesService companieservice, ICountyService countyservice, ISliderService sliderService, ICityService cityservice)
        {
            this._sliderService = sliderService;
            this._cityservice = cityservice;
            this._countyservice = countyservice;
            this._companieservice = companieservice;
            this._neighborhoodservice = neighborhoodservice;
            this._productservice = productservice;
            this._categoryservice = categoryservice;
            this._menunameservice = menunameservice;
            _cacheProvider = new CacheProvider();


        }
        public ActionResult Index(int cityId)
        {
          
            var veriler = _sliderService.GetAll();
            List<string> resimler = new List<string>();
            for (int i = 0; i < veriler.Count(); i++)
            {
                var base64 = Convert.ToBase64String(veriler[i].SliderFoto);
                var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                resimler.Add(imagesrc);
            }
            ViewBag.resimler = resimler;
            Session["sehirid"] = cityId;
            foreach (var item in _cityservice.GetAll())
            {
                if (cityId==item.Id)
                {
                    Session["sehiradi"] = item.CityName;
                }
            }
            List<SelectListItem> mahalleler = new List<SelectListItem>();
            List<string> ilcevemahalle = new List<string>();
            Ilcevesemt ilcevemahalles = new Ilcevesemt();
            ilcevemahalles.semtveilceid = new List<string>();
            ilcevemahalles.semtveilcebilgi = new List<string>();
            TempData["secilenil"] = cityId;
            foreach (var item in _countyservice.Getiril(cityId))
            {
               
                foreach (var item2 in _neighborhoodservice.Getir(item.Id))
                {
                   
                    if (item2!=null)
                    {
                        ilcevemahalle.Add(item.CountyName + " (" + item2.NeighborhoodName+" Mah. )");
                        ilcevemahalles.semtveilceid.Add(item.Id + "," + item2.Id);
                        ilcevemahalles.semtveilcebilgi.Add(item.CountyName + " ( " + item2.NeighborhoodName + " Mah. ) ");
                    } 
                }
            }
            ViewBag.bilgiler = ilcevemahalle;
            TempData["listemiz"] = ilcevemahalle;
            int sayac = 1;
            for (int i=0;i<ilcevemahalles.semtveilcebilgi.Count();i++)
            {
                mahalleler.Add(new SelectListItem { Text =ilcevemahalles.semtveilcebilgi[i], Value =ilcevemahalles.semtveilceid[i] });
                sayac = sayac + 1;
            }
            ViewBag.mahalleler = mahalleler;
       
            return View(ilcevemahalles);
        }

      
        public PartialViewResult DövizKurlari()
        {
            Merkezbankasi verileri = new Merkezbankasi();
            string bugun = "http://www.tcmb.gov.tr/kurlar/today.xml";
            var xmldokuman = new XmlDocument();
            xmldokuman.Load(bugun);
            DateTime tarih = Convert.ToDateTime(xmldokuman.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value);
            string usd = xmldokuman.SelectSingleNode("Tarih_Date/Currency [@Kod='USD']/BanknoteSelling").InnerXml;
            // var dolar = string.Format("Tarih{0} USD;{1}", tarih.ToShortDateString(), usd);

            string euro = xmldokuman.SelectSingleNode("Tarih_Date/Currency [@Kod='EUR']/BanknoteSelling").InnerXml;
            // var dolar = string.Format("Tarih{0} USD;{1}", tarih.ToShortDateString(), usd);
            
            verileri.dolar = usd;
            verileri.euro = euro;
            return PartialView(verileri);
        }
        public ActionResult AramaSonuclari(Ilcevesemt gelenler)
        {

            var categorylistesi = _categoryservice.GetAll();
            var categoriler =
               categorylistesi.OrderBy(kategori => kategori.Cname)
                         .GroupBy(kategori => kategori.Cname)
                         .Select(bilgiler => new { bilgiler = bilgiler.Key, OgrenciSayisi = bilgiler.Count() });
            Categorilistebilgi veri = new Categorilistebilgi();
            veri.Categoryliste = new List<Category>();
            veri.resimbilgilistesi = new List<string>();
            Session["bilgiler"] = gelenler;
            var veriler = TempData["gelenlerbilgisi"];
            foreach (var item in categoriler)
            {
                var categoriresim = _categoryservice.Getircategorisim(item.bilgiler);
                if (categoriresim.CResim!= null)
                {
                    var base64 = Convert.ToBase64String(categoriresim.CResim);
                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                    veri.resimbilgilistesi.Add(imagesrc);
                }
                if (categoriresim.CResim==null)
                {
                    veri.resimbilgilistesi.Add(null);
                }
                veri.Categoryliste.Add(categoriresim);
            }
            ViewBag.categoribilgi = veri;
            var acikfirmalar = Session["acikfirmalar"];
            var mintutar = Session["mintutar"];
            var odeme = Session["odemetut"];
            var hizimiz = Session["hizimiz"];
            var lezzeti = Session["lezzeti"];
           
            Session["acikfirmalarmiz"] = acikfirmalar;
            Session["mintutarimiz"] = mintutar;
            Session["odemetutari"] = odeme;
            Session["hizim"] = hizimiz;
            Session["lezzetim"] = lezzeti;
            
            TimeSpan suankisaat = TimeSpan.Parse(DateTime.Now.Hour.ToString());
           
            string[] parcalar;
            parcalar = gelenler.secilensemt.Split(',');
            List<Companies> firmalar = new List<Companies>();
            List<string> firmaresim = new List<string>();
           

            foreach (var item in _companieservice.GetAll())
            {
                if (item.CountyId == Convert.ToInt32(parcalar[0]))
                {
                    if (item.NeighborhoodId == Convert.ToInt32(parcalar[1]))
                    {

                        if (acikfirmalar == null && mintutar == null && odeme == null && hizimiz == null && lezzeti == null)
                        {
                            firmalar.Add(item);
                            if (item.CompanyLogo != null)
                            {
                                var base64 = Convert.ToBase64String(item.CompanyLogo);
                                var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                firmaresim.Add(imagesrc);
                            }
                            if (item.CompanyLogo == null)
                            {
                                firmaresim.Add(null);
                            }

                        }
                        if (acikfirmalar != null && mintutar != null && odeme != null && hizimiz != null && lezzeti != null)
                        {
                            var acikfirmalar2 = (bool)Session["acikfirmalar"];
                            var mintutar2 = (string)Session["mintutar"];
                            var odeme2 = (string)Session["odemetut"];
                            var hizimiz2 = (string)Session["hizimiz"];
                            var lezzeti2 = (string)Session["lezzeti"];
                            var acilistring = item.OpeningTime.ToString();
                            var kapanistring = item.ClosingTime.ToString();
                            TimeSpan acilisaati = TimeSpan.Parse(acilistring);
                            TimeSpan kapanisaati = TimeSpan.Parse(kapanistring);
                            var simdikisaat = DateTime.Now.ToLongTimeString();
                            TimeSpan simdisaat = TimeSpan.Parse(simdikisaat);
                            if (acikfirmalar2 == true && string.Equals(mintutar2,"Hepsi")==true && string.Equals(odeme2, "Hepsi") == true  && string.Equals(hizimiz2,"Hepsi") == true && string.Equals(lezzeti2,"Hepsi")==true)
                            {
                                if (acilisaati <simdisaat  && kapanisaati> simdisaat)
                                {
                                   
                                        firmalar.Add(item);
                                        if (item.CompanyLogo != null)
                                        {
                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                            firmaresim.Add(imagesrc);
                                        }
                                        if (item.CompanyLogo == null)
                                        {
                                            firmaresim.Add(null);
                                        }
                                }
                            }
                            if (acikfirmalar2 == false && string.Equals(mintutar2, "Hepsi") == false && string.Equals(odeme2, "Hepsi") == true && string.Equals(hizimiz2, "Hepsi") == true && string.Equals(lezzeti2, "Hepsi") == true)
                            {
                                if (acilisaati > simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <= (item.MinimumPackagePrice))
                                    {
                                        firmalar.Add(item);
                                        if (item.CompanyLogo != null)
                                        {
                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                            firmaresim.Add(imagesrc);
                                        }
                                        if (item.CompanyLogo == null)
                                        {
                                            firmaresim.Add(null);
                                        }
                                    }
                                }
                                if (acilisaati < simdisaat && kapanisaati < simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <= (item.MinimumPackagePrice))
                                    {
                                        firmalar.Add(item);
                                        if (item.CompanyLogo != null)
                                        {
                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                            firmaresim.Add(imagesrc);
                                        }
                                        if (item.CompanyLogo == null)
                                        {
                                            firmaresim.Add(null);
                                        }
                                    }
                                }

                            }
                            if (acikfirmalar2 == false && string.Equals(mintutar2, "Hepsi") == true && string.Equals(odeme2, "Hepsi") == false && string.Equals(hizimiz2, "Hepsi") == true && string.Equals(lezzeti2, "Hepsi") == true)
                            {
                                if (acilisaati > simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                    foreach (var items in odemeparcala)
                                    {
                                        if (items == odeme2)
                                        {
                                            firmalar.Add(item);
                                            if (item.CompanyLogo != null)
                                            {
                                                var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                firmaresim.Add(imagesrc);
                                            }
                                            if (item.CompanyLogo == null)
                                            {
                                                firmaresim.Add(null);
                                            }
                                        }
                                    }
                                }
                                if (acilisaati < simdisaat && kapanisaati < simdisaat)
                                {
                                    string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                    foreach (var items in odemeparcala)
                                    {
                                        if (items == odeme2)
                                        {
                                            firmalar.Add(item);
                                            if (item.CompanyLogo != null)
                                            {
                                                var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                firmaresim.Add(imagesrc);
                                            }
                                            if (item.CompanyLogo == null)
                                            {
                                                firmaresim.Add(null);
                                            }
                                        }
                                    }
                                }
                            }

                            if (acikfirmalar2 == false && string.Equals(mintutar2, "Hepsi") == true && string.Equals(odeme2, "Hepsi") == true && string.Equals(hizimiz2, "Hepsi") == false && string.Equals(lezzeti2, "Hepsi") == true)
                            {
                                if (acilisaati > simdisaat && kapanisaati > simdisaat)
                                {

                                    var hizi = hizimiz2.Split(' ');
                                    var hizint = (hizi[0]).ToString();
                                    var hizlar = hizint.Replace('.', ',');
                                    var hizdouble = Convert.ToDecimal(hizlar);
                                    Session["hiz2int"] = hizdouble;
                                    if (hizdouble <= item.SpeedPoint)
                                    {
                                        firmalar.Add(item);
                                        if (item.CompanyLogo != null)
                                        {
                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                            firmaresim.Add(imagesrc);
                                        }
                                        if (item.CompanyLogo == null)
                                        {
                                            firmaresim.Add(null);
                                        }
                                    }
                                }
                                if (acilisaati < simdisaat && kapanisaati < simdisaat)
                                {
                                    var hizi = hizimiz2.Split(' ');
                                    var hizint = (hizi[0]).ToString();
                                    var hizlar = hizint.Replace('.', ',');
                                    var hizdouble = Convert.ToDecimal(hizlar);
                                    Session["hiz2int"] = hizdouble;
                                    if (hizdouble <=item.SpeedPoint)
                                    {
                                        firmalar.Add(item);
                                        if (item.CompanyLogo != null)
                                        {
                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                            firmaresim.Add(imagesrc);
                                        }
                                        if (item.CompanyLogo == null)
                                        {
                                            firmaresim.Add(null);
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == false && string.Equals(mintutar2, "Hepsi") == true && string.Equals(odeme2, "Hepsi") == true && string.Equals(hizimiz2, "Hepsi") == true && string.Equals(lezzeti2, "Hepsi") == false)
                            {
                                if (acilisaati > simdisaat && kapanisaati > simdisaat)
                                {
                                    var lezzetin = lezzeti2.Split(' ');
                                    var lezzetint = (lezzetin[0]).ToString();
                                    var lezzetler = lezzetint.Replace('.', ',');
                                    var lezzetdouble = Convert.ToDecimal(lezzetler);
                                    Session["lezzet2int"] = lezzetdouble;
                                    if (lezzetdouble <= item.FlavorPoint)
                                    {
                                        firmalar.Add(item);
                                        if (item.CompanyLogo != null)
                                        {
                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                            firmaresim.Add(imagesrc);
                                        }
                                        if (item.CompanyLogo == null)
                                        {
                                            firmaresim.Add(null);
                                        }
                                    }
                                }
                                if (acilisaati < simdisaat && kapanisaati < simdisaat)
                                {
                                    var lezzetin = lezzeti2.Split(' ');
                                    var lezzetint = (lezzetin[0]).ToString();
                                    var lezzetler = lezzetint.Replace('.', ',');
                                    var lezzetdouble = Convert.ToDecimal(lezzetler);
                                    Session["lezzet2int"] = lezzetdouble;
                                    if (lezzetdouble <= item.FlavorPoint)
                                    {
                                        firmalar.Add(item);
                                        if (item.CompanyLogo != null)
                                        {
                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                            firmaresim.Add(imagesrc);
                                        }
                                        if (item.CompanyLogo == null)
                                        {
                                            firmaresim.Add(null);
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == true && string.Equals(mintutar2, "Hepsi") == false && string.Equals(odeme2, "Hepsi") == true && string.Equals(hizimiz2, "Hepsi") == true && string.Equals(lezzeti2, "Hepsi") == true)
                            {
                              
                                if (acilisaati < simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <= (item.MinimumPackagePrice))
                                    {
                                        firmalar.Add(item);
                                        if (item.CompanyLogo != null)
                                        {
                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                            firmaresim.Add(imagesrc);
                                        }
                                        if (item.CompanyLogo == null)
                                        {
                                            firmaresim.Add(null);
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == true && string.Equals(mintutar2, "Hepsi") == true && string.Equals(odeme2, "Hepsi") == false && string.Equals(hizimiz2, "Hepsi") == true && string.Equals(lezzeti2, "Hepsi") == true)
                            {
                                if (acilisaati < simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                    foreach (var items in odemeparcala)
                                    {
                                        if (items == odeme2)
                                        {
                                            firmalar.Add(item);
                                            if (item.CompanyLogo != null)
                                            {
                                                var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                firmaresim.Add(imagesrc);
                                            }
                                            if (item.CompanyLogo == null)
                                            {
                                                firmaresim.Add(null);
                                            }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == true && string.Equals(mintutar2, "Hepsi") == true && string.Equals(odeme2, "Hepsi") == true && string.Equals(hizimiz2, "Hepsi") == false && string.Equals(lezzeti2, "Hepsi") == true)
                            {
                                if (acilisaati < simdisaat && kapanisaati > simdisaat)
                                {
                                    var hizi = hizimiz2.Split(' ');
                                    var hizint = (hizi[0]).ToString();
                                    var hizlar = hizint.Replace('.', ',');
                                    var hizdouble = Convert.ToDecimal(hizlar);
                                    Session["hiz2int"] = hizdouble;
                                    if (hizdouble <= item.SpeedPoint)
                                    {
                                        firmalar.Add(item);
                                        if (item.CompanyLogo != null)
                                        {
                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                            firmaresim.Add(imagesrc);
                                        }
                                        if (item.CompanyLogo == null)
                                        {
                                            firmaresim.Add(null);
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 ==true && string.Equals(mintutar2, "Hepsi") == true && string.Equals(odeme2, "Hepsi") == true && string.Equals(hizimiz2, "Hepsi") == true && string.Equals(lezzeti2, "Hepsi") == false)
                            {
                                if (acilisaati < simdisaat && kapanisaati > simdisaat)
                                {
                                    var lezzetin = lezzeti2.Split(' ');
                                    var lezzetint = (lezzetin[0]).ToString();
                                    var lezzetler = lezzetint.Replace('.', ',');
                                    var lezzetdouble = Convert.ToDecimal(lezzetler);
                                    Session["lezzet2int"] = lezzetdouble;
                                    if (lezzetdouble <= item.FlavorPoint)
                                    {
                                        firmalar.Add(item);
                                        if (item.CompanyLogo != null)
                                        {
                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                            firmaresim.Add(imagesrc);
                                        }
                                        if (item.CompanyLogo == null)
                                        {
                                            firmaresim.Add(null);
                                        }
                                    }
                                }
                            }
                         
                            if (acikfirmalar2 == false && string.Equals(mintutar2, "Hepsi") == false && string.Equals(odeme2, "Hepsi") == false && string.Equals(hizimiz2, "Hepsi") == true && string.Equals(lezzeti2, "Hepsi") == true)
                            {
                                if (acilisaati > simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <=(item.MinimumPackagePrice))
                                    {
                                        string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                        foreach (var items in odemeparcala)
                                        {
                                            if (items == odeme2)
                                            {
                                                firmalar.Add(item);
                                                if (item.CompanyLogo != null)
                                                {
                                                    var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                    firmaresim.Add(imagesrc);
                                                }
                                                if (item.CompanyLogo == null)
                                                {
                                                    firmaresim.Add(null);
                                                }
                                            }
                                        }
                                    }
                                }
                                if (acilisaati < simdisaat && kapanisaati < simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <= (item.MinimumPackagePrice))
                                    {
                                        string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                        foreach (var items in odemeparcala)
                                        {
                                            if (items == odeme2)
                                            {
                                                firmalar.Add(item);
                                                if (item.CompanyLogo != null)
                                                {
                                                    var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                    firmaresim.Add(imagesrc);
                                                }
                                                if (item.CompanyLogo == null)
                                                {
                                                    firmaresim.Add(null);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == false && string.Equals(mintutar2, "Hepsi") == false && string.Equals(odeme2, "Hepsi") == true && string.Equals(hizimiz2, "Hepsi") == false && string.Equals(lezzeti2, "Hepsi") == true)
                            {
                                if (acilisaati > simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <=(item.MinimumPackagePrice))
                                        {
                                                var hizi = hizimiz2.Split(' ');
                                                var hizint = (hizi[0]).ToString();
                                                var hizlar = hizint.Replace('.', ',');
                                                var hizdouble = Convert.ToDecimal(hizlar);
                                                Session["hiz2int"] = hizdouble;
                                                if (hizdouble <= item.SpeedPoint)
                                                    {
                                                        firmalar.Add(item);
                                                        if (item.CompanyLogo != null)
                                                        {
                                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                            firmaresim.Add(imagesrc);
                                                        }
                                                        if (item.CompanyLogo == null)
                                                        {
                                                            firmaresim.Add(null);
                                                        }
                                                  }
                                           }
                               }
                                if (acilisaati < simdisaat && kapanisaati < simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <=(item.MinimumPackagePrice))
                                    {
                                        var hizi = hizimiz2.Split(' ');
                                        var hizint = (hizi[0]).ToString();
                                        var hizlar = hizint.Replace('.', ',');
                                        var hizdouble = Convert.ToDecimal(hizlar);
                                        Session["hiz2int"] = hizdouble;
                                        if (hizdouble <= item.SpeedPoint)
                                        {
                                            firmalar.Add(item);
                                            if (item.CompanyLogo != null)
                                            {
                                                var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                firmaresim.Add(imagesrc);
                                            }
                                            if (item.CompanyLogo == null)
                                            {
                                                firmaresim.Add(null);
                                            }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == false && string.Equals(mintutar2, "Hepsi") == false && string.Equals(odeme2, "Hepsi") == true && string.Equals(hizimiz2, "Hepsi") == true && string.Equals(lezzeti2, "Hepsi") == false)
                            {
                                
                                if (acilisaati > simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <= (item.MinimumPackagePrice))
                                    {
                                        var lezzetin = lezzeti2.Split(' ');
                                        var lezzetint = (lezzetin[0]).ToString();
                                        var lezzetler = lezzetint.Replace('.', ',');
                                        var lezzetdouble = Convert.ToDecimal(lezzetler);
                                        Session["lezzet2int"] = lezzetdouble;
                                        if (lezzetdouble <= item.FlavorPoint)
                                        {
                                            firmalar.Add(item);
                                            if (item.CompanyLogo != null)
                                            {
                                                var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                firmaresim.Add(imagesrc);
                                            }
                                            if (item.CompanyLogo == null)
                                            {
                                                firmaresim.Add(null);
                                            }
                                        }
                                    }
                                }
                                if (acilisaati < simdisaat && kapanisaati < simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <=(item.MinimumPackagePrice))
                                    {
                                        var lezzetin = lezzeti2.Split(' ');
                                        var lezzetint = (lezzetin[0]).ToString();
                                        var lezzetler = lezzetint.Replace('.', ',');
                                        var lezzetdouble = Convert.ToDecimal(lezzetler);
                                        Session["lezzet2int"] = lezzetdouble;
                                        if (lezzetdouble <= item.FlavorPoint)
                                        {
                                            firmalar.Add(item);
                                            if (item.CompanyLogo != null)
                                            {
                                                var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                firmaresim.Add(imagesrc);
                                            }
                                            if (item.CompanyLogo == null)
                                            {
                                                firmaresim.Add(null);
                                            }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == false && string.Equals(mintutar2, "Hepsi") == true && string.Equals(odeme2, "Hepsi") == false && string.Equals(hizimiz2, "Hepsi") == false && string.Equals(lezzeti2, "Hepsi") == true)
                            {
                                if (acilisaati > simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                    foreach (var items in odemeparcala)
                                    {
                                        if (items == odeme2)
                                        {
                                            var hizi = hizimiz2.Split(' ');
                                            var hizint = (hizi[0]).ToString();
                                            var hizlar = hizint.Replace('.', ',');
                                            var hizdouble = Convert.ToDecimal(hizlar);
                                            Session["hiz2int"] = hizdouble;
                                            if (hizdouble <= item.SpeedPoint)
                                            {
                                                firmalar.Add(item);
                                                if (item.CompanyLogo != null)
                                                {
                                                    var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                    firmaresim.Add(imagesrc);
                                                }
                                                if (item.CompanyLogo == null)
                                                {
                                                    firmaresim.Add(null);
                                                }
                                            }
                                        }
                                    }
                                }
                                if (acilisaati < simdisaat && kapanisaati < simdisaat)
                                {
                                    string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                    foreach (var items in odemeparcala)
                                    {
                                        if (items == odeme2)
                                        {
                                            var hizi = hizimiz2.Split(' ');
                                            var hizint = (hizi[0]).ToString();
                                            var hizlar = hizint.Replace('.', ',');
                                            var hizdouble = Convert.ToDecimal(hizlar);
                                            Session["hiz2int"] = hizdouble;
                                            if (hizdouble <= item.SpeedPoint)
                                            {
                                                firmalar.Add(item);
                                                if (item.CompanyLogo != null)
                                                {
                                                    var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                    firmaresim.Add(imagesrc);
                                                }
                                                if (item.CompanyLogo == null)
                                                {
                                                    firmaresim.Add(null);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == false && string.Equals(mintutar2, "Hepsi") == true && string.Equals(odeme2, "Hepsi") == false && string.Equals(hizimiz2, "Hepsi") == true && string.Equals(lezzeti2, "Hepsi") == false)
                            {
                            
                                if (acilisaati > simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                    foreach (var items in odemeparcala)
                                    {
                                        if (items == odeme2)
                                        {
                                            var lezzetin = lezzeti2.Split(' ');
                                            var lezzetint = (lezzetin[0]).ToString();
                                            var lezzetler = lezzetint.Replace('.', ',');
                                            var lezzetdouble = Convert.ToDecimal(lezzetler);
                                            Session["lezzet2int"] = lezzetdouble;
                                            if (lezzetdouble <= item.FlavorPoint)
                                            {
                                                firmalar.Add(item);
                                                if (item.CompanyLogo != null)
                                                {
                                                    var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                    firmaresim.Add(imagesrc);
                                                }
                                                if (item.CompanyLogo == null)
                                                {
                                                    firmaresim.Add(null);
                                                }
                                            }
                                        }
                                    }
                                }
                                if (acilisaati < simdisaat && kapanisaati < simdisaat)
                                {
                                    string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                    foreach (var items in odemeparcala)
                                    {
                                        if (items == odeme2)
                                        {
                                            var lezzetin = lezzeti2.Split(' ');
                                            var lezzetint = (lezzetin[0]).ToString();
                                            var lezzetler = lezzetint.Replace('.', ',');
                                            var lezzetdouble = Convert.ToDecimal(lezzetler);
                                            Session["lezzet2int"] = lezzetdouble;
                                            if (lezzetdouble <= item.FlavorPoint)
                                            {
                                                firmalar.Add(item);
                                                if (item.CompanyLogo != null)
                                                {
                                                    var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                    firmaresim.Add(imagesrc);
                                                }
                                                if (item.CompanyLogo == null)
                                                {
                                                    firmaresim.Add(null);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == true && string.Equals(mintutar2, "Hepsi") == false && string.Equals(odeme2, "Hepsi") == false && string.Equals(hizimiz2, "Hepsi") == true && string.Equals(lezzeti2, "Hepsi") == true)
                            {
                                if (acilisaati < simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <= (item.MinimumPackagePrice))
                                    {
                                      string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                        foreach (var items in odemeparcala)
                                        {
                                            if (items == odeme2)
                                            {
                                                firmalar.Add(item);
                                                if (item.CompanyLogo != null)
                                                {
                                                    var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                    firmaresim.Add(imagesrc);
                                                }
                                                if (item.CompanyLogo == null)
                                                {
                                                    firmaresim.Add(null);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == true && string.Equals(mintutar2, "Hepsi") == false && string.Equals(odeme2, "Hepsi") == true && string.Equals(hizimiz2, "Hepsi") == false && string.Equals(lezzeti2, "Hepsi") == true)
                            {
                                if (acilisaati < simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <= (item.MinimumPackagePrice))
                                    {
                                        var hizi = hizimiz2.Split(' ');
                                        var hizint = (hizi[0]).ToString();
                                        var hizlar = hizint.Replace('.', ',');
                                        var hizdouble = Convert.ToDecimal(hizlar);
                                        Session["hiz2int"] = hizdouble;
                                        if (hizdouble <= item.SpeedPoint)
                                        {
                                            firmalar.Add(item);
                                                if (item.CompanyLogo != null)
                                                {
                                                    var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                    firmaresim.Add(imagesrc);
                                                }
                                                if (item.CompanyLogo == null)
                                                {
                                                    firmaresim.Add(null);
                                                }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == true && string.Equals(mintutar2, "Hepsi") == false && string.Equals(odeme2, "Hepsi") == true && string.Equals(hizimiz2, "Hepsi") == true && string.Equals(lezzeti2, "Hepsi") == false)
                            {
                                if (acilisaati < simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <=(item.MinimumPackagePrice))
                                    {
                                        var lezzetin = lezzeti2.Split(' ');
                                        var lezzetint = (lezzetin[0]).ToString();
                                        var lezzetler = lezzetint.Replace('.', ',');
                                        var lezzetdouble = Convert.ToDecimal(lezzetler);
                                        Session["lezzet2int"] = lezzetdouble;
                                        if (lezzetdouble <= item.FlavorPoint)
                                        {
                                            firmalar.Add(item);
                                            if (item.CompanyLogo != null)
                                            {
                                                var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                firmaresim.Add(imagesrc);
                                            }
                                            if (item.CompanyLogo == null)
                                            {
                                                firmaresim.Add(null);
                                            }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == false && string.Equals(mintutar2, "Hepsi") == false && string.Equals(odeme2, "Hepsi") == false && string.Equals(hizimiz2, "Hepsi") == false && string.Equals(lezzeti2, "Hepsi") == true)
                            {
                                if (acilisaati > simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <=(item.MinimumPackagePrice))
                                    {
                                        string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                        foreach (var items in odemeparcala)
                                        {
                                            if (items == odeme2)
                                            {
                                                var hizi = hizimiz2.Split(' ');
                                                var hizint = (hizi[0]).ToString();
                                                var hizlar = hizint.Replace('.', ',');
                                                var hizdouble = Convert.ToDecimal(hizlar);
                                                Session["hiz2int"] = hizdouble;
                                                if (hizdouble <= item.SpeedPoint)
                                                {
                                                    firmalar.Add(item);
                                                    if (item.CompanyLogo != null)
                                                    {
                                                        var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                        var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                        firmaresim.Add(imagesrc);
                                                    }
                                                    if (item.CompanyLogo == null)
                                                    {
                                                        firmaresim.Add(null);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (acilisaati < simdisaat && kapanisaati < simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <=(item.MinimumPackagePrice))
                                    {
                                        string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                        foreach (var items in odemeparcala)
                                        {
                                            if (items == odeme2)
                                            {
                                                var hizi = hizimiz2.Split(' ');
                                                var hizint = (hizi[0]).ToString();
                                                var hizlar = hizint.Replace('.', ',');
                                                var hizdouble = Convert.ToDecimal(hizlar);
                                                Session["hiz2int"] = hizdouble;
                                                if (hizdouble <= item.SpeedPoint)
                                                {
                                                    firmalar.Add(item);
                                                    if (item.CompanyLogo != null)
                                                    {
                                                        var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                        var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                        firmaresim.Add(imagesrc);
                                                    }
                                                    if (item.CompanyLogo == null)
                                                    {
                                                        firmaresim.Add(null);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == false && string.Equals(mintutar2, "Hepsi") == false && string.Equals(odeme2, "Hepsi") == false && string.Equals(hizimiz2, "Hepsi") == true && string.Equals(lezzeti2, "Hepsi") == false)
                            {
                                if (acilisaati > simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <=(item.MinimumPackagePrice))
                                    {
                                        string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                        foreach (var items in odemeparcala)
                                        {
                                            if (items == odeme2)
                                            {
                                                var lezzetin = lezzeti2.Split(' ');
                                                var lezzetint = (lezzetin[0]).ToString();
                                                var lezzetler = lezzetint.Replace('.', ',');
                                                var lezzetdouble = Convert.ToDecimal(lezzetler);
                                                Session["lezzet2int"] = lezzetdouble;
                                                if (lezzetdouble <= item.FlavorPoint)
                                                {
                                                    firmalar.Add(item);
                                                    if (item.CompanyLogo != null)
                                                    {
                                                        var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                        var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                        firmaresim.Add(imagesrc);
                                                    }
                                                    if (item.CompanyLogo == null)
                                                    {
                                                        firmaresim.Add(null);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (acilisaati < simdisaat && kapanisaati < simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <=(item.MinimumPackagePrice))
                                    {
                                        string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                        foreach (var items in odemeparcala)
                                        {
                                            if (items == odeme2)
                                            {
                                                var lezzetin = lezzeti2.Split(' ');
                                                var lezzetint = (lezzetin[0]).ToString();
                                                var lezzetler = lezzetint.Replace('.', ',');
                                                var lezzetdouble = Convert.ToDecimal(lezzetler);
                                                Session["lezzet2int"] = lezzetdouble;
                                                if (lezzetdouble <= item.FlavorPoint)
                                                {
                                                    firmalar.Add(item);
                                                    if (item.CompanyLogo != null)
                                                    {
                                                        var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                        var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                        firmaresim.Add(imagesrc);
                                                    }
                                                    if (item.CompanyLogo == null)
                                                    {
                                                        firmaresim.Add(null);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == false && string.Equals(mintutar2, "Hepsi") == true && string.Equals(odeme2, "Hepsi") == false && string.Equals(hizimiz2, "Hepsi") == false && string.Equals(lezzeti2, "Hepsi") == false)
                            {
                                if (acilisaati > simdisaat && kapanisaati > simdisaat)
                                {
                                    var hizi = hizimiz2.Split(' ');
                                    var hizint = (hizi[0]).ToString();
                                    var hizlar = hizint.Replace('.', ',');
                                    var hizdouble = Convert.ToDecimal(hizlar);
                                    Session["hiz2int"] = hizdouble;
                                    if (hizdouble <= item.SpeedPoint)
                                    {
                                        string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                        foreach (var items in odemeparcala)
                                        {
                                            if (items == odeme2)
                                            {
                                                var lezzetin = lezzeti2.Split(' ');
                                                var lezzetint = (lezzetin[0]).ToString();
                                                var lezzetler = lezzetint.Replace('.', ',');
                                                var lezzetdouble = Convert.ToDecimal(lezzetler);
                                                Session["lezzet2int"] = lezzetdouble;
                                                if (lezzetdouble <= item.FlavorPoint)
                                                {
                                                    firmalar.Add(item);
                                                    if (item.CompanyLogo != null)
                                                    {
                                                        var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                        var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                        firmaresim.Add(imagesrc);
                                                    }
                                                    if (item.CompanyLogo == null)
                                                    {
                                                        firmaresim.Add(null);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (acilisaati < simdisaat && kapanisaati < simdisaat)
                                {
                                    var hizi = hizimiz2.Split(' ');
                                    var hizint = (hizi[0]).ToString();
                                    var hizlar = hizint.Replace('.', ',');
                                    var hizdouble = Convert.ToDecimal(hizlar);
                                    Session["hiz2int"] = hizdouble;
                                    if (hizdouble <= item.SpeedPoint)
                                    {
                                        string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                        foreach (var items in odemeparcala)
                                        {
                                            if (items == odeme2)
                                            {
                                                var lezzetin = lezzeti2.Split(' ');
                                                var lezzetint = (lezzetin[0]).ToString();
                                                var lezzetler = lezzetint.Replace('.', ',');
                                                var lezzetdouble = Convert.ToDecimal(lezzetler);
                                                Session["lezzet2int"] = lezzetdouble;
                                                if (lezzetdouble <= item.FlavorPoint)
                                                {
                                                    firmalar.Add(item);
                                                    if (item.CompanyLogo != null)
                                                    {
                                                        var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                        var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                        firmaresim.Add(imagesrc);
                                                    }
                                                    if (item.CompanyLogo == null)
                                                    {
                                                        firmaresim.Add(null);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == true && string.Equals(mintutar2, "Hepsi") == false && string.Equals(odeme2, "Hepsi") == false && string.Equals(hizimiz2, "Hepsi") == false && string.Equals(lezzeti2, "Hepsi") == true)
                            {
                                if (acilisaati < simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <=(item.MinimumPackagePrice))
                                    {
                                        string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                        foreach (var items in odemeparcala)
                                        {
                                            if (items == odeme2)
                                            {
                                                var hizi = hizimiz2.Split(' ');
                                                var hizint = (hizi[0]).ToString();
                                                var hizlar = hizint.Replace('.', ',');
                                                var hizdouble = Convert.ToDecimal(hizlar);
                                                Session["hiz2int"] = hizdouble;
                                                if (hizdouble <= item.SpeedPoint)
                                                {
                                                    firmalar.Add(item);
                                                    if (item.CompanyLogo != null)
                                                    {
                                                        var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                        var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                        firmaresim.Add(imagesrc);
                                                    }
                                                    if (item.CompanyLogo == null)
                                                    {
                                                        firmaresim.Add(null);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == false && string.Equals(mintutar2, "Hepsi") == false && string.Equals(odeme2, "Hepsi") == false && string.Equals(hizimiz2, "Hepsi") == false && string.Equals(lezzeti2, "Hepsi") == false)
                            {
                                if (acilisaati > simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <=(item.MinimumPackagePrice))
                                    {
                                        var hizi = hizimiz2.Split(' ');
                                        var hizint = (hizi[0]).ToString();
                                        var hizlar = hizint.Replace('.', ',');
                                        var hizdouble = Convert.ToDecimal(hizlar);
                                        Session["hiz2int"] = hizdouble;
                                        if (hizdouble <= item.SpeedPoint)
                                        {
                                            string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                            foreach (var items in odemeparcala)
                                            {
                                                if (items == odeme2)
                                                {
                                                    var lezzetin = lezzeti2.Split(' ');
                                                    var lezzetint = (lezzetin[0]).ToString();
                                                    var lezzetler = lezzetint.Replace('.', ',');
                                                    var lezzetdouble = Convert.ToDecimal(lezzetler);
                                                    Session["lezzet2int"] = lezzetdouble;
                                                    if (lezzetdouble <= item.FlavorPoint)
                                                    {
                                                        firmalar.Add(item);
                                                        if (item.CompanyLogo != null)
                                                        {
                                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                            firmaresim.Add(imagesrc);
                                                        }
                                                        if (item.CompanyLogo == null)
                                                        {
                                                            firmaresim.Add(null);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                if (acilisaati < simdisaat && kapanisaati < simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <=(item.MinimumPackagePrice))
                                    {
                                        var hizi = hizimiz2.Split(' ');
                                        var hizint = (hizi[0]).ToString();
                                        var hizlar = hizint.Replace('.', ',');
                                        var hizdouble = Convert.ToDecimal(hizlar);
                                        Session["hiz2int"] = hizdouble;
                                        if (hizdouble <= item.SpeedPoint)
                                        {
                                            string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                            foreach (var items in odemeparcala)
                                            {
                                                if (items == odeme2)
                                                {
                                                    var lezzetin = lezzeti2.Split(' ');
                                                    var lezzetint = (lezzetin[0]).ToString();
                                                    var lezzetler = lezzetint.Replace('.', ',');
                                                    var lezzetdouble = Convert.ToDecimal(lezzetler);
                                                    Session["lezzet2int"] = lezzetdouble;
                                                    if (lezzetdouble <= item.FlavorPoint)
                                                    {
                                                        firmalar.Add(item);
                                                        if (item.CompanyLogo != null)
                                                        {
                                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                            firmaresim.Add(imagesrc);
                                                        }
                                                        if (item.CompanyLogo == null)
                                                        {
                                                            firmaresim.Add(null);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (acikfirmalar2 == true && string.Equals(mintutar2, "Hepsi") == false && string.Equals(odeme2, "Hepsi") == false && string.Equals(hizimiz2, "Hepsi") == false && string.Equals(lezzeti2, "Hepsi") == false)
                            {
                                if (acilisaati < simdisaat && kapanisaati > simdisaat)
                                {
                                    string[] mintutari = mintutar2.Split(' ');
                                    var mintutarint = Convert.ToInt32(mintutari[0]);
                                    Session["min2tutar"] = Convert.ToInt32(mintutarint);
                                    if (mintutarint <=(item.MinimumPackagePrice))
                                    {
                                        string[] odemeparcala = item.PaymentOptionsId.Split(',');
                                        foreach (var items in odemeparcala)
                                        {
                                            if (items == odeme2)
                                            {
                                                var lezzetin = lezzeti2.Split(' ');
                                                var lezzetint = (lezzetin[0]).ToString();
                                                var lezzetler = lezzetint.Replace('.', ',');
                                                var lezzetdouble = Convert.ToDecimal(lezzetler);
                                                Session["lezzet2int"] = lezzetdouble;
                                                if (lezzetdouble <= item.FlavorPoint)
                                                {
                                                    var hizi = hizimiz2.Split(' ');
                                                    var hizint = (hizi[0]).ToString();
                                                    var hizlar = hizint.Replace('.', ',');
                                                    var hizdouble = Convert.ToDecimal(hizlar);
                                                    Session["hiz2int"] = hizdouble;
                                                    if (hizdouble <= item.SpeedPoint)
                                                    {
                                                        firmalar.Add(item);
                                                        if (item.CompanyLogo != null)
                                                        {
                                                            var base64 = Convert.ToBase64String(item.CompanyLogo);
                                                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                                            firmaresim.Add(imagesrc);
                                                        }
                                                        if (item.CompanyLogo == null)
                                                        {
                                                            firmaresim.Add(null);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                ViewBag.listeler = firmalar;
                ViewBag.firmaresim = firmaresim;

               
            }
            return View(gelenler);
        }
        public ActionResult SepetDetay()
        {
             if (Session["adsoyadi"] == null && Session["kullaniciadi"] == null)
             {
                 return RedirectToAction("Uyeol","Home");
             }
            if (Session["adsoyadi"] != null && Session["kullaniciadi"] != null)
            {


            return View((Sepet)HttpContext.Session["AktifSepet"]);
            }

             return View();
        }
        public ActionResult Filtreleme(bool acikfirmalar = false, string mintut = "Hepsi", string odeme = "Hepsi", string hiz = "Hepsi", string lezzet = "Hepsi",string gelenbilgi="")
        {
            Session["acikfirmalar"] = acikfirmalar;
            Session["mintutar"] = mintut;
            Session["odemetut"] = odeme;
            Session["hizimiz"] = hiz;
            Session["lezzeti"] = lezzet;
            var gelenler = TempData["gelenlerbilgisi"];
            
            return RedirectToAction("Deneme");
        }


        public ActionResult YemekveRestoransearch(string search = "")
        {

            var categorylistesi = _categoryservice.GetAll();
            var categoriler =
               categorylistesi.OrderBy(kategori => kategori.Cname)
                         .GroupBy(kategori => kategori.Cname)
                         .Select(bilgiler => new { bilgiler = bilgiler.Key, OgrenciSayisi = bilgiler.Count() });
            Categorilistebilgi verim = new Categorilistebilgi();
            verim.Categoryliste = new List<Category>();
            verim.resimbilgilistesi = new List<string>();
            foreach (var item in categoriler)
            {
                var categoriresim = _categoryservice.Getircategorisim(item.bilgiler);
                if (categoriresim.CResim != null)
                {
                    var base64 = Convert.ToBase64String(categoriresim.CResim);
                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                    verim.resimbilgilistesi.Add(imagesrc);
                }
                if (categoriresim.CResim == null)
                {
                    verim.resimbilgilistesi.Add(null);
                }
                verim.Categoryliste.Add(categoriresim);
            }
            TempData["aramasonucu"] = search;
            Session["aramabilgisi"] = search;
            ViewBag.categoribilgi = verim;
            Ilcevesemt veriler = (Ilcevesemt)Session["bilgiler"];
            var ilcevesemtler = veriler.secilensemt;
            string[] parcalar;
            parcalar = ilcevesemtler.Split(',');
            List<Companies> firmalar = new List<Companies>();
            List<string> firmaresim = new List<string>();
            Firmamenuveurunlistesi veri = new Firmamenuveurunlistesi();
            veri.Companies = new List<Companies>();
            veri.Category = new List<Category>();
            veri.MenuNames = new List<MenuNames>();
            veri.Product = new List<Product>();
            veri.Resimler = new List<string>();
                if (search == "")
                {
                    foreach (var item in _companieservice.GetAll())
                    {
                        if (item.CountyId == Convert.ToInt32(parcalar[0]))
                        {
                            if (item.NeighborhoodId == Convert.ToInt32(parcalar[1]))
                            {

                                veri.Companies.Add(item);
                               
                                if (item.CompanyLogo != null)
                                {
                                    var base64 = Convert.ToBase64String(item.CompanyLogo);
                                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                    veri.Resimler.Add(imagesrc);
                                }
                                if (item.CompanyLogo == null)
                                {
                                    veri.Resimler.Add(null);
                                }
                            }
                        }
                    }
                }
            if (string.Equals(search, "") == false)
	        {
                foreach (var item in _companieservice.GetAll())
                {
                    foreach (var item2 in _menunameservice.GetAll())
                    {
                        if (item.CountyId == Convert.ToInt32(parcalar[0]))
                        {

                            if (item.NeighborhoodId == Convert.ToInt32(parcalar[1]))
                            {
                                int icerisindevar = item2.MenuName.IndexOf(search, 0, item2.MenuName.Length);
                                if (icerisindevar != -1 && item.Id == item2.CompaniesId)
                                {
                                    veri.MenuNames.Add(item2);
                                    for (int i = 0; i < veri.Companies.Count(); i++)
                                    {
                                        if (veri.Companies[i].CompanyName != item.CompanyName)
                                        {
                                            veri.Companies.Add(item);
                                        }
                                    }
                                    if (veri.Companies.Count == 0)
                                    {
                                        veri.Companies.Add(item);
                                    }
                                    if (item.CompanyLogo != null)
                                    {
                                        var base64 = Convert.ToBase64String(item.CompanyLogo);
                                        var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                        veri.Resimler.Add(imagesrc);
                                    }
                                    if (item.CompanyLogo == null)
                                    {
                                        veri.Resimler.Add(null);
                                    }
                                    icerisindevar = -1;

                                }
                            }
                        }
                    }
                }
              
                foreach (var item in _companieservice.GetAll())
                {
                    foreach (var item2 in _productservice.GetAll())
                    {
                        if (item.CountyId == Convert.ToInt32(parcalar[0]))
                        {

                            if (item.NeighborhoodId == Convert.ToInt32(parcalar[1]))
                            {
                                int icerisindevar = item2.Pname.IndexOf(search, 0, item2.Pname.Length);
                                if (icerisindevar != -1 && item.Id == item2.CompaniesId)
                                {
                                    veri.Product.Add(item2);
                                    for (int i = 0; i < veri.Companies.Count(); i++)
                                    {
                                        if (veri.Companies[i].CompanyName != item.CompanyName)
                                        {
                                            veri.Companies.Add(item);
                                        }
                                    }
                                    if (veri.Companies.Count == 0)
                                    {
                                        veri.Companies.Add(item);
                                    }
                                    if (item.CompanyLogo != null)
                                    {
                                        var base64 = Convert.ToBase64String(item.CompanyLogo);
                                        var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                        veri.Resimler.Add(imagesrc);
                                    }
                                    if (item.CompanyLogo == null)
                                    {
                                        veri.Resimler.Add(null);
                                    }
                                    icerisindevar = -1;
                                }
                            }
                        }
                    }
                }
            }
               
              
                   
           return View(veri);
        }

        [HttpPost]
      public JsonResult SeachArama(string searchvalue)
      {
            //burasida searchın yonledırdıgı
            var firmalar=_companieservice.GetAll();
            var yemekler = _productservice.GetAll();
            var categori = _categoryservice.GetAll();
            int startIndex = 0;
            int length = searchvalue.Length;
            Searchler veriler = new Searchler();
            veriler.CategoryListe = new List<Category>();
            veriler.ProductListe = new List<Product>();
            veriler.FirmaListesi = new List<Companies>();
          
            foreach (var item in firmalar)
            {
                if (item.CompanyName.Length>length)
                {
                    String elemanlar = item.CompanyName.Substring(startIndex, length).ToLower();
                    if (elemanlar.Equals(searchvalue))
                    {
                        veriler.FirmaListesi.Add(item);
                    }
                }
              
            }
            foreach (var item in yemekler)
            {
                if (item.Pname.Length>length)
                {
                    String elemanlar = item.Pname.Substring(startIndex, length).ToLower();
                    if (elemanlar.Equals(searchvalue))
                    {
                        veriler.ProductListe.Add(item);
                    }
                }
              
            }
            foreach (var item in categori)
            {
                if (item.Cname.Length>length)
                {
                    String elemanlar = item.Cname.Substring(startIndex, length).ToLower();
                    if (elemanlar.Equals(searchvalue))
                    {
                        veriler.CategoryListe.Add(item);
                    }
                }
               
            } 
           return Json(veriler,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Deneme()
        {
         
            
            return View();
        }
        public ActionResult Deneme3()
        {
            return View();
        }
        public ActionResult Den()
        {
            Ilcevesemt veriler = (Ilcevesemt)Session["bilgiler"];
           
            return RedirectToAction("AramaSonuclari",veriler);
        }
    }
}