using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using YemekSepetiUygulamasi.Bll;
using YemekSepetiUygulamasi.Bll.Abstract;
using YemekSepetiUygulamasi.Entity.EntityTable;
using YemekSepetiUygulamasi.WebUI.Areas.Admin.Models;
using YemekSepetiUygulamasi.WebUI.Models;
using PagedList;
using PagedList.Mvc;
using System.Text;
using Rotativa.MVC;
using System.IO;

namespace YemekSepetiUygulamasi.WebUI.Areas.Admin.Controllers
{
    public class AnasayfaController : Controller
    {
        IManagerTableService _managertableservice;
        IPromosyonService _promosyonservice;
        ICustomerService _customerservice;
        ICustomersAdressService _customeradresservice;
        ISifreDegisiklikService _sifredegisiklikservice;
        ICompaniesService _companiesservice;
        ICityService _cityservice;
        ICountyService _countyservice;
        INeighborhoodService _neighborhoodservice;
        ICommentService _commentservice;
        IOrderService _orderservice;
        IOrderProductService _orderproductservice;
        IDiscountCouponService _discountcouponservice;
        IMenuNamesService _menunamesservice;
        ICampaignPNameService _campaignproductnameservice;
        IProductService _productservice;
        IStatusService _statusservice;
        IMenusService _menusservice;
        IProductImageService _productimageservice;
        ICampaignProductsService _campaingproductservice;
        public AnasayfaController(ICampaignProductsService campaingproductservice, IPromosyonService promosyonservice, IProductImageService productimageservice, IMenusService menusservice, IStatusService statusservice,IProductService productservice, ICampaignPNameService campaignproductnameservice, IMenuNamesService menunamesservice, IDiscountCouponService discountcouponservice, IOrderService orderservice,IOrderProductService orderproductservice,ICommentService commentservice, INeighborhoodService neighborhoodservice, ICountyService countyservice, ICompaniesService companiesservice,IManagerTableService managertableservice,ISifreDegisiklikService sifredegisiklikservice, ISliderService sliderService, ICityService cityservice, ICustomerService customerservice, ICustomersAdressService customeradresservice)
        {
            
            this._customerservice = customerservice;
            this._customeradresservice = customeradresservice;
            this._sifredegisiklikservice = sifredegisiklikservice;
            this._managertableservice = managertableservice;
            this._companiesservice = companiesservice;
            this._cityservice = cityservice;
            this._countyservice = countyservice;
            this._neighborhoodservice = neighborhoodservice;
            this._commentservice = commentservice;
            this._orderservice = orderservice;
            this._orderproductservice = orderproductservice;
            this._discountcouponservice = discountcouponservice;
            this._productservice = productservice;
            this._campaignproductnameservice = campaignproductnameservice;
            this._menunamesservice = menunamesservice;
            this._statusservice = statusservice;
            this._menusservice = menusservice;
            this._productimageservice = productimageservice;
            this._promosyonservice = promosyonservice;
            this._campaingproductservice = campaingproductservice;
        }
        // GET: Admin/Anasayfa
        public ActionResult Index()
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            { 
            var girisyapan = Session["KullaniciAdi"];
            var firmaadi = Session["firmaadi"].ToString();
            var firma = _companiesservice.Get(firmaadi);
            return View(firma);
           }
        }
        public ActionResult Sayac(int id)
        {
            TempData["deger"] = id + 1;
            return RedirectToAction("MenuEkle");
        }
        public ActionResult UrunKaldir(int id)
        {
            TempData["deger"] = id - 1;
            return RedirectToAction("MenuEkle");
        }
        public ActionResult MenuEkle()

        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                Menubilgilerimodel veri = new Menubilgilerimodel();
                veri.MenuNames = new MenuNames();
                veri.Menusliste = new List<Menus>();
                veri.Productsectikleri = new List<Product>();
                veri.Menusliste = new List<Menus>();
                if(TempData["deger"]==null)
                {
                    veri.sayac = 1;
                }
                else
                {
                    veri.sayac = Convert.ToInt32(TempData["deger"]);
                }
               
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                veri.MenuNames.CompaniesId = firma.Id;
                veri.Productliste = _productservice.Geturun(firma.Id);
                List<SelectListItem> urunler = new List<SelectListItem>();
               
                foreach (var item in _productservice.Geturun(firma.Id))
                { 
                    urunler.Add(new SelectListItem { Text = item.Pname, Value = item.Id.ToString() });
                }
              
                ViewBag.urunler = urunler;

                return View(veri);
            }
        }
        [HttpPost]
        public ActionResult MenuEkle(Menubilgilerimodel veri)
        {
            
            MenuNames kayit = new MenuNames();
            kayit.MenuResim = new byte[veri.menuresim.ContentLength];
            veri.menuresim.InputStream.Read(kayit.MenuResim, 0, veri.menuresim.ContentLength);
            kayit.MenuName = veri.MenuNames.MenuName;
            kayit.Price = decimal.Parse(veri.fiyat.Replace(".", ","));
            kayit.CompaniesId = veri.MenuNames.CompaniesId;
            kayit.Description = veri.MenuNames.Description;
            _menunamesservice.Add(kayit);
            var elemanlar = _menunamesservice.GetAll();
            var soneleman = elemanlar.Last();
           
            foreach (var item in veri.Productsectikleri)
            {
                Menus menu = new Menus();
                menu.CompaniesId = veri.MenuNames.CompaniesId;
                menu.MenuNamesId = soneleman.Id;
                menu.ProductId = item.Id;
                _menusservice.Add(menu);
            }
            return RedirectToAction("MenuBilgileri");
        }
        [HttpPost]
        public ActionResult KampanyaEkle(KampanyaBilgilerimodel veri)
        {
            CampaignPName kayit = new CampaignPName();
            kayit.CampaignResim = new byte[veri.kampanyaresim.ContentLength];
            veri.kampanyaresim.InputStream.Read(kayit.CampaignResim, 0, veri.kampanyaresim.ContentLength);
            kayit.CampaignName = veri.CampaignPName.CampaignName;
            kayit.Price = decimal.Parse(veri.fiyat.Replace(".", ","));
            kayit.CompaniesId = veri.CampaignPName.CompaniesId;
            kayit.Description = veri.CampaignPName.Description;
            _campaignproductnameservice.Add(kayit);
            var girisyapan = Session["KullaniciAdi"];
            var firmaadi = Session["firmaadi"].ToString();
            var firma = _companiesservice.Get(firmaadi);
            var elemanlar = _campaignproductnameservice.Getirliste(firma.Id);
            var soneleman = elemanlar.Last();

            foreach (var item in veri.Productsectikleri)
            {
                CampaignProduct  kampanyamenu= new CampaignProduct();
                kampanyamenu.CompaniesId = veri.CampaignPName.CompaniesId;
                kampanyamenu.CampaignPNameId = soneleman.Id;
                kampanyamenu.ProductId = item.Id;
                _campaingproductservice.Add(kampanyamenu);
            }


            return RedirectToAction("KampanyaBilgileri");
          
        }
        public ActionResult KampanyaEkle()
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                KampanyaBilgilerimodel veri = new KampanyaBilgilerimodel();
                veri.CampaignPName = new CampaignPName();
                veri.Productliste = new List<Product>();
                veri.Productsectikleri = new List<Product>();
                veri.CampaignProductliste = new List<CampaignProduct>();
                if (TempData["deger"] == null)
                {
                    veri.sayac = 1;
                }
                else
                {
                    veri.sayac = Convert.ToInt32(TempData["deger"]);
                }
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                veri.CampaignPName.CompaniesId = firma.Id;
                List<SelectListItem> urunler = new List<SelectListItem>();
                foreach (var item in _productservice.Geturun(firma.Id))
                {
                    urunler.Add(new SelectListItem { Text = item.Pname, Value = item.Id.ToString() });
                }

                ViewBag.urunler = urunler;

                return View(veri);
               

            }
        }
        public ActionResult Sayac2(int id)
        {
            TempData["deger"] = id + 1;
            return RedirectToAction("KampanyaEkle");
        }
        public ActionResult UrunKaldir2(int id)
        {
            TempData["deger"] = id - 1;
            return RedirectToAction("KampanyaEkle");
        }
        public ActionResult KampanyaBilgileri()
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                CampanyaveresimlerViewModel model = new CampanyaveresimlerViewModel();
                model.CampanyaveresimlerModel = new List<CampanyaveresimlerModel>();
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                var kampanyalar = _campaignproductnameservice.Getirliste(firma.Id);

                for (int i = 0; i < kampanyalar.Count(); i++)
                {
                    CampanyaveresimlerModel veri = new CampanyaveresimlerModel();
                    if (kampanyalar[i].CampaignResim != null)
                    {
                        var base64 = Convert.ToBase64String(kampanyalar[i].CampaignResim);
                        var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                        veri.Resimler = imagesrc;
                    }
                    else
                    {
                        veri.Resimler = null;

                    }
                    veri.CampaignPName = kampanyalar[i];
                    model.CampanyaveresimlerModel.Add(veri);
                }
                return View(model);
               
            }
        }
        public ActionResult MenuBilgileri()
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                MenuveresimlerViewModel model = new MenuveresimlerViewModel();
                model.Menubilgilerimodel = new List<Menuveresimlermodel>();
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                var menuler = _menunamesservice.Getirliste(firma.Id);
               
                for (int i = 0; i < menuler.Count(); i++)
                {
                       Menuveresimlermodel veri = new Menuveresimlermodel();
                    if (menuler[i].MenuResim != null)
                    {
                        var base64 = Convert.ToBase64String(menuler[i].MenuResim);
                        var imagesrc = string.Format("data:image/gif;base64,{0}", base64); 
                        veri.Resimler = imagesrc;
                    }
                    else
                    {
                        veri.Resimler = null;

                    }
                    veri.MenuNames = menuler[i];
                    model.Menubilgilerimodel.Add(veri);
                }
             

                return View(model);
            }
        }

        public ActionResult KampanyaIncele(int Id)
        {
            Session["deger"] = 0;
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                List<CampaignProduct> veriler = new List<CampaignProduct>();
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                var kampanyaname = _campaignproductnameservice.Get(Id);
                var kampanyaurun = _campaingproductservice.Getir(firma.Id, Id);
                CampanyawithProduct kampanya = new CampanyawithProduct();
                kampanya.CampaignPName = new CampaignPName();
                kampanya.CampaignProduct = new List<CampaignProduct>();
                kampanya.Product = new List<Product>();
                kampanya.ProductImage = new List<ProductImage>();
                kampanya.CampaignPName = kampanyaname;
                if (kampanyaname.CampaignResim != null)
                {
                    var base64 = Convert.ToBase64String(kampanyaname.CampaignResim);
                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                    kampanya.kampanyaresim = imagesrc;
                }
                else
                {
                    kampanya.kampanyaresim = null;
                }
                for (int i = 0; i < kampanyaurun.Count(); i++)
                {
                    veriler.Add(kampanyaurun[i]);

                    kampanya.CampaignProduct.Add(kampanyaurun[i]);
                }
                for (int i = 0; i < kampanyaurun.Count(); i++)
                {
                    Product deger = new Product();
                    ProductImage resim = new ProductImage();
                    deger = _productservice.Get((int)kampanyaurun[i].ProductId);
                    kampanya.Product.Add(deger);
                    if (deger.ProductImageId != null)
                    {
                        resim = _productimageservice.Get((int)deger.ProductImageId);
                        kampanya.ProductImage.Add(resim);
                    }


                }
                return View(kampanya);
            }
        }
        public ActionResult MenuIncele(int Id)
        {
            Session["deger"] = 0;
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                List<Menus> veriler = new List<Menus>();
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                var menuname = _menunamesservice.Get(Id);
                var menu = _menusservice.Getir(firma.Id, Id);
                MenuwithProduct veri = new MenuwithProduct();
                veri.MenuNames = new MenuNames();
                veri.Menus = new List<Menus>();
                veri.Product = new List<Product>();
                veri.ProductImage = new List<ProductImage>();
                veri.MenuNames = menuname;
                if (menuname.MenuResim != null)
                {
                    var base64 = Convert.ToBase64String(menuname.MenuResim);
                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                    veri.menuresim = imagesrc;
                }
                else
                {
                    veri.menuresim = null;

                }
                for (int i = 0; i < menu.Count(); i++)
                {
                    veriler.Add(menu[i]);

                    veri.Menus.Add(menu[i]);
                }

                for (int i = 0; i < menu.Count(); i++)
                {
                    Product deger = new Product();
                    ProductImage resim = new ProductImage();
                    deger = _productservice.Get((int)menu[i].ProductId);
                    veri.Product.Add(deger);
                    if (deger.ProductImageId != null)
                    {
                        resim = _productimageservice.Get((int)deger.ProductImageId);
                        veri.ProductImage.Add(resim);
                    }


                }
                return View(veri);
            }
        }


        public ActionResult KampanyaUrunleri(int tiklananid, int kampanyaid)
        {

            var girisyapan = Session["KullaniciAdi"];
            var firmaadi = Session["firmaadi"].ToString();
            var firma = _companiesservice.Get(firmaadi);
            var kampanyaname = _campaignproductnameservice.GetirBilgi(firma.Id, kampanyaid);
            var kampanyaurun = _campaingproductservice.Getir(firma.Id, kampanyaid);
            List<Product> urun = new List<Product>();


            List<int> productid = new List<int>();

            var urunler = _productservice.Geturun(firma.Id);
            List<int> odanumarasi = new List<int>();

            ArrayList liste = new ArrayList();
            for (int i = 0; i < urunler.Count(); i++)
            {
                liste.Add(urunler[i].Id);
            }
            for (int i = 0; i < kampanyaurun.Count(); i++)
            {
                liste.Add((int)kampanyaurun[i].ProductId);
            }
            int sayacdegeri = 0;
            List<Product> urunle = new List<Product>();
            for (int i = 0; i < liste.Count; i++)
            {
                sayacdegeri = 0;
                for (int j = 0; j < liste.Count; j++)
                {
                    if ((int)liste[i] == (int)liste[j])
                    {
                        sayacdegeri = sayacdegeri + 1;

                    }
                }
                if (sayacdegeri == 1)
                {
                    int urunid = (int)liste[i];
                    var urunbilgisi = _productservice.Get(urunid);
                    urunle.Add(urunbilgisi);
                }
            }
            ViewBag.veri = urunle;
            ViewBag.tiklananid = tiklananid;
            ViewBag.kampanyaid = kampanyaid;
            return View();
        }
        public ActionResult MenuUrunleri(int tiklananid, int menuid)
        {

            var girisyapan = Session["KullaniciAdi"];
            var firmaadi = Session["firmaadi"].ToString();
            var firma = _companiesservice.Get(firmaadi);
            var menuname = _menunamesservice.GetirBilgi(firma.Id,menuid);
            var menu = _menusservice.Getir(firma.Id, menuid);
            List<Product> urun = new List<Product>();
         
       
            List<int> productid = new List<int>();

            var urunler = _productservice.GetAll();
            List<int> odanumarasi = new List<int>();
            
            ArrayList liste = new ArrayList();
            for (int i = 0; i <urunler.Count(); i++)
            {
                liste.Add(urunler[i].Id);
            }
            for (int i = 0; i < menu.Count(); i++)
            {
                liste.Add((int)menu[i].ProductId);
            }
            int sayacdegeri = 0;
            List<Product> urunle = new List<Product>();
            for (int i = 0; i < liste.Count; i++)
            {
                sayacdegeri = 0;
                for (int j = 0; j < liste.Count; j++)
                {
                    if ((int)liste[i] == (int)liste[j])
                    {
                        sayacdegeri = sayacdegeri + 1;

                    }
                }
                if (sayacdegeri == 1)
                {
                    int urunid = (int)liste[i];
                    var urunbilgisi = _productservice.Get(urunid);
                    urunle.Add(urunbilgisi);
                }
            }
            ViewBag.veri = urunle;
            ViewBag.tiklananid = tiklananid;
            ViewBag.menuid = menuid;
            return View();
        }
        public ActionResult KampanyaurunSecim(int tiklananurunid, int yeniurunid, int menuid)
        {
            List<CampaignProduct> veriler = new List<CampaignProduct>();
            var girisyapan = Session["KullaniciAdi"];
            var firmaadi = Session["firmaadi"].ToString();
            var firma = _companiesservice.Get(firmaadi);
            var kampanyaurun = _campaingproductservice.Getir(firma.Id, menuid);


            var tiklananurunidbil = tiklananurunid;
            var yeniurunidbil = yeniurunid;

            for (int i = 0; i < kampanyaurun.Count(); i++)
            {
                if (tiklananurunidbil == kampanyaurun[i].ProductId)
                {
                    var tiklama = _campaingproductservice.Get(kampanyaurun[i].Id);
                    CampaignProduct tiklanan = new CampaignProduct();
                    tiklanan.Id = tiklama.Id;
                    tiklanan.CampaignPNameId = tiklama.CampaignPNameId;
                    tiklanan.ProductId = yeniurunid;
                    tiklanan.CompaniesId = tiklama.CompaniesId;
                    _campaingproductservice.Update(tiklanan);
                }
            }

            return RedirectToAction("KampanyaDuzenle", new { Id = menuid });
        }
        public ActionResult MenurunSecim(int tiklananurunid,int yeniurunid,int menuid)
        {
            List<Menus> veriler = new List<Menus>();
            var girisyapan = Session["KullaniciAdi"];
            var firmaadi = Session["firmaadi"].ToString();
            var firma = _companiesservice.Get(firmaadi);
            var menu = _menusservice.Getir(firma.Id, menuid);
           
           
                var tiklananurunidbil = tiklananurunid;
                var yeniurunidbil = yeniurunid;
                
                for (int i = 0; i < menu.Count(); i++)
                {
                    if (tiklananurunidbil == menu[i].ProductId)
                    {
                        var tiklama = _menusservice.Get(menu[i].Id);
                        Menus tiklanan = new Menus();
                        tiklanan.Id = tiklama.Id;
                        tiklanan.MenuNamesId = tiklama.MenuNamesId;
                        tiklanan.ProductId = yeniurunid;
                        tiklanan.CompaniesId = tiklama.CompaniesId;
                        _menusservice.Update(tiklanan);
                    }
                }
            
            return RedirectToAction("MenuDuzenle",new {Id=menuid});
          
        }
        public ActionResult KampanyaDuzenle(int Id)
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                List<CampaignProduct> veriler = new List<CampaignProduct>();
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                var kampanyaname = _campaignproductnameservice.GetirBilgi(firma.Id, Id);
                var kampanyaurun = _campaingproductservice.Getir(firma.Id, Id);
                CampanyawithProduct veri = new CampanyawithProduct();
                veri.CampaignPName = new CampaignPName();
                veri.CampaignProduct = new List<CampaignProduct>();
                veri.Product = new List<Product>();
                veri.ProductImage = new List<ProductImage>();
                veri.Resimler = new List<string>();
                veri.kacincieleman = new List<int>();
                veri.CampaignPName = kampanyaname;
                if (Session["veri"] == null && Session["deger2"] == null)
                {
                    veri.sayacdegeri = 0;
                    veri.kacincieleman.DefaultIfEmpty();
                }
                if (kampanyaname.CampaignResim != null)
                {
                    var base64 = Convert.ToBase64String(kampanyaname.CampaignResim);
                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                    veri.kampanyaresim = imagesrc;
                }
                else
                {
                    veri.kampanyaresim = null;
                }
                for (int i = 0; i < kampanyaurun.Count(); i++)
                {

                    veriler.Add(kampanyaurun[i]);
                    veri.CampaignProduct.Add(kampanyaurun[i]);
                }
                for (int i = 0; i < kampanyaurun.Count(); i++)
                {

                    ProductImage resim = new ProductImage();
                    resim = _productimageservice.Getir(firma.Id, (int)kampanyaurun[i].ProductId);


                    if (resim.Resimler != null)
                    {
                        var base64 = Convert.ToBase64String(resim.Resimler);
                        var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                        veri.Resimler.Add(imagesrc);
                    }
                    if (resim.Resimler == null)
                    {
                        veri.Resimler.Add(null);
                    }

                }

                var campanyaproduct = _campaingproductservice.Getir(firma.Id, Id);
                for (int i = 0; i < campanyaproduct.Count(); i++)
                {
                    Product deger = new Product();
                    ProductImage resim = new ProductImage();
                    deger = _productservice.Get((int)campanyaproduct[i].ProductId);
                    veri.Product.Add(deger);
                    if (deger.ProductImageId != null)
                    {
                        resim = _productimageservice.Get((int)deger.ProductImageId);
                        veri.ProductImage.Add(resim);
                    }
                    else
                    {
                        veri.ProductImage.Add(null);
                    }
                }
                var verimler = _productservice.Geturun(firma.Id);

                List<int> productid = new List<int>();

                var products = _productservice.GetAll();
                List<int> odanumarasi = new List<int>();

                ArrayList liste = new ArrayList();
                for (int i = 0; i < products.Count(); i++)
                {
                    liste.Add(products[i].Id);
                }
                for (int i = 0; i < kampanyaurun.Count(); i++)
                {
                    liste.Add((int)kampanyaurun[i].ProductId);
                }
                int sayacdegeri = 0;
                List<Product> urunle = new List<Product>();
                for (int i = 0; i < liste.Count; i++)
                {
                    sayacdegeri = 0;
                    for (int j = 0; j < liste.Count; j++)
                    {
                        if ((int)liste[i] == (int)liste[j])
                        {
                            sayacdegeri = sayacdegeri + 1;

                        }
                    }
                    if (sayacdegeri == 1)
                    {
                        int urunid = (int)liste[i];
                        var urunbilgisi = _productservice.Get(urunid);
                        urunle.Add(urunbilgisi);
                    }
                }
                ViewBag.veri = urunle;


                List<SelectListItem> urunler = new List<SelectListItem>();
                foreach (var item in _productservice.GetAll())
                {
                    urunler.Add(new SelectListItem { Text = item.Pname, Value = item.Id.ToString() });
                }

                ViewBag.urunler = urunler;
                return View(veri);
         
            }
        }
        public ActionResult MenuDuzenle(int Id)
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                List<Menus> veriler = new List<Menus>();
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                var menuname = _menunamesservice.GetirBilgi(firma.Id, Id);
                var menu = _menusservice.Getir(firma.Id, Id);
                MenuwithProduct veri = new MenuwithProduct();
                veri.MenuNames = new MenuNames();
                veri.Menus = new List<Menus>();
                veri.Product = new List<Product>();
                veri.ProductImage = new List<ProductImage>();
                veri.Resimler = new List<string>();
                veri.kacincieleman = new List<int>();
                veri.MenuNames = menuname;
                
                if (Session["veri"] == null && Session["deger2"]==null)
                {
                    veri.sayacdegeri = 0;
                    veri.kacincieleman.DefaultIfEmpty();
                }
                if (menuname.MenuResim != null)
                {
                    var base64 = Convert.ToBase64String(menuname.MenuResim);
                    var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                    veri.menuresim = imagesrc;
                }
                else
                {
                    veri.menuresim = null;
                }
                for (int i = 0; i < menu.Count(); i++)
                {
                    
                    veriler.Add(menu[i]);
                    veri.Menus.Add(menu[i]);
                }
                for (int i = 0; i < menu.Count(); i++)
                {

                    ProductImage resim = new ProductImage();
                    resim = _productimageservice.Getir(firma.Id, (int)menu[i].ProductId);
                    

                    if (resim.Resimler != null)
                    {
                        var base64 = Convert.ToBase64String(resim.Resimler);
                        var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                        veri.Resimler.Add(imagesrc);
                    }
                    if (resim.Resimler == null)
                    {
                        veri.Resimler.Add(null);
                    }

                }
           
                var menuler = _menusservice.Getir(firma.Id, Id);
                for (int i = 0; i < menuler.Count(); i++)
                    {
                        Product deger = new Product();
                        ProductImage resim = new ProductImage();
                        deger = _productservice.Get((int)menu[i].ProductId);
                        veri.Product.Add(deger);
                        if (deger.ProductImageId != null)
                        {
                            resim = _productimageservice.Get((int)deger.ProductImageId);
                            veri.ProductImage.Add(resim);
                        }
                        else
                        {
                            veri.ProductImage.Add(null);
                        }
                    }
                var verimler = _productservice.GetAll();
                
                List<int> productid = new List<int>();
              
                var products = _productservice.GetAll();
                List<int> odanumarasi = new List<int>();
                
                ArrayList liste = new ArrayList();
                for (int i = 0; i < products.Count(); i++)
                {
                    liste.Add(products[i].Id);
                }
                for (int i = 0; i < menu.Count(); i++)
                {
                    liste.Add((int)menu[i].ProductId);
                }
                int sayacdegeri = 0;
                List<Product> urunle = new List<Product>();
                for (int i = 0; i < liste.Count; i++)
                {
                    sayacdegeri = 0;
                    for (int j = 0; j < liste.Count; j++)
                    {
                        if ((int)liste[i]==(int)liste[j])
                        {
                            sayacdegeri = sayacdegeri + 1;
                            
                        }
                    } 
                    if (sayacdegeri==1)
                    {
                        int urunid = (int)liste[i];
                        var urunbilgisi = _productservice.Get(urunid);
                        urunle.Add(urunbilgisi);
                    } 
                  }
                    ViewBag.veri = urunle;


                List<SelectListItem> urunler = new List<SelectListItem>();
                foreach (var item in _productservice.GetAll())
                {
                    urunler.Add(new SelectListItem { Text = item.Pname, Value = item.Id.ToString() });
                }

                ViewBag.urunler = urunler;
                return View(veri);
            }
        }
        

        [HttpPost]
        public ActionResult MenuSil(int Id)
        {
            var girisyapan = Session["KullaniciAdi"];
            var firmaadi = Session["firmaadi"].ToString();
            var firma = _companiesservice.Get(firmaadi);
            var menunames = _menunamesservice.Get(Id);
            var menus = _menusservice.Getir(firma.Id,Id);
            _menunamesservice.Delete(menunames);
            for (int i = 0; i < menus.Count(); i++)
            {
                var veri=_menusservice.Get(menus[i].Id);
                _menusservice.Delete(veri);
            }
            return RedirectToAction("MenuBilgileri");
        }
        [HttpPost]
        public ActionResult KampanyaSil(int Id)
        {
            var girisyapan = Session["KullaniciAdi"];
            var firmaadi = Session["firmaadi"].ToString();
            var firma = _companiesservice.Get(firmaadi);
            var campaignames = _campaignproductnameservice.Get(Id);
            var campaigproduct = _campaingproductservice.Getir(firma.Id, Id);
            _campaignproductnameservice.Delete(campaignames);
            for (int i = 0; i < campaigproduct.Count(); i++)
            {
                var veri = _campaingproductservice.Get(campaigproduct[i].Id);
                _campaingproductservice.Delete(veri);
            }
            return RedirectToAction("KampanyaBilgileri");
        }
        public ActionResult Incele(int Id)
        {
            var secilen = _companiesservice.Get(Id);
            var iller = _cityservice.GetAll();
            var ilceler = _countyservice.GetAll();
            var semtler = _neighborhoodservice.GetAll();
            for (int i = 0; i < iller.Count(); i++)
            {
                if (iller[i].Id==secilen.CityId)
                {
                    TempData["iladi"] = iller[i].CityName.ToUpper();
                }
            }
            for (int j = 0; j <ilceler.Count(); j++)
            {
                if (ilceler[j].Id==secilen.CountyId)
                {
                   TempData["ilceadi"] = ilceler[j].CountyName.ToUpper();
                }
            }
            for (int k = 0; k < semtler.Count(); k++)
            {
                if(semtler[k].Id==secilen.NeighborhoodId)
                {
                    TempData["semtadi"] = semtler[k].NeighborhoodName.ToUpper();
                }
            }
            return View(secilen);
        }
        
      
        public ActionResult Duzenle(int Id)
        {
            var iller = _cityservice.GetAll();
            var ilceler = _countyservice.GetAll();
            var semtler = _neighborhoodservice.GetAll();
            ViewBag.iller = iller;
            TempData["ilceler"] = ilceler;
            TempData["semtler"] = semtler;
            var duzenlenecek = _companiesservice.Get(Id);
            ViewBag.illiste = new SelectList(GetCityList(), "Id", "CityName");
            CasctingClass db = new CasctingClass();
            db.Companies = duzenlenecek;
            return View(db);
        }
        [HttpPost]
        public ActionResult Duzenle(CasctingClass gelenler)
        {
            Companies veri = new Companies();
            veri = gelenler.Companies;
            veri.CityId = gelenler.CityId;
            veri.CountyId = gelenler.CountyId;
            veri.NeighborhoodId = gelenler.NeighborhoodId;
            veri.Id = gelenler.Companies.Id;
            _companiesservice.Update(veri);

            return RedirectToAction("Index");
        }
        public ActionResult UrunBilgileri()
        {
            return View();
        }
        public List<City> GetCityList()
        {
            List<City> liste = _cityservice.GetAll();
            return liste;
        }
        [HttpPost]
        public ActionResult GetCountyList(int Id)
        {
            List<County> selectlist = _countyservice.Getir(Id);
            ViewBag.slist = new SelectList(selectlist, "Id", "CountyName");
            return PartialView("DisplayCounty");
        }
        [HttpPost]
        public ActionResult GetNeighList(int Id)
        {
            List<Neighborhood> selectlist = _neighborhoodservice.Getir(Id);
            ViewBag.clist = new SelectList(selectlist, "Id", "NeighborhoodName");
            return PartialView("DisplayNeighborhood");
        }
      
        public ActionResult Siparisincele(int Id)
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var siparis = _orderservice.Get(Id);
                var orderproduct = _orderproductservice.Getorder(siparis.Id);
                Siparislerim veri = new Siparislerim();
                veri.MenuNames = new List<MenuNames>();
                veri.CampaignProductName = new List<CampaignPName>();
                veri.OrderProduct = new List<OrderProduct>();
                veri.Product = new List<Product>();
                veri.Order = siparis;

                var siparissahibi = _customerservice.Get((int)siparis.CustomerId);
                var siparissahibadress = _customeradresservice.Get((int)siparis.CustomerAddressId);
                var siparisdurum = _statusservice.Get((int)siparis.StatusId);
                veri.Status = siparisdurum;
                veri.Customer = siparissahibi;
                veri.CustomersAdress = siparissahibadress;
                if (siparis.DiscountCouponId != null)
                {
                    var indirimkuponu = _discountcouponservice.Get((int)siparis.DiscountCouponId);
                    veri.DiscountCoupon = indirimkuponu;
                }
                var orderproducturunleri = _orderproductservice.Getorder(siparis.Id);
                for (int k = 0; k < orderproducturunleri.Count(); k++)
                {
                    veri.OrderProduct.Add(orderproducturunleri[k]);
                    List<MenuNames> menuadi = new List<MenuNames>();
                    if (orderproducturunleri[k].MenuNamesId == null)
                    {
                        veri.MenuNames.Add(null);
                    }
                    if (orderproducturunleri[k].MenuNamesId != null)
                    {
                        var menunames = _menunamesservice.Get((int)orderproducturunleri[k].MenuNamesId);
                        veri.MenuNames.Add(menunames);
                    }
                    if (orderproducturunleri[k].CampaignProductId == null)
                    {
                        veri.CampaignProductName.Add(null);
                    }
                    if (orderproducturunleri[k].CampaignProductId != null)
                    {
                        var kampanyaname = _campaignproductnameservice.Get((int)orderproducturunleri[k].CampaignProductId);
                        veri.CampaignProductName.Add(kampanyaname);
                    }
                    if (orderproducturunleri[k].ProductId == null)
                    {
                        veri.Product.Add(null);
                    }
                    if (orderproducturunleri[k].ProductId != null)
                    {
                        var urunname = _productservice.Get((int)orderproducturunleri[k].ProductId);
                        veri.Product.Add(urunname);
                    }
                }

                return View(veri);
            }
        }
        public ActionResult Siparisler()
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                var siparis = _orderservice.Getir(firma.Id);
                SiparislerimViewModel model = new SiparislerimViewModel();
                model.Siparisislemleri = new List<Siparisislemleri>();
                List<MenuNames> menuurun = new List<MenuNames>();
                List<Product> veriler = new List<Product>();
                List<Menus> menus = new List<Menus>();
                double toplamtutar = 0;
                double indirimlitutar = 0;
                for (int i = 0; i < siparis.Count(); i++)
                {
                    Siparisislemleri bs = new Siparisislemleri();
                    bs.OrderProduct = new List<OrderProduct>();
                    bs.Order = siparis[i];
                    Customer kisi = new Customer();
                    kisi = _customerservice.Get((int)siparis[i].CustomerId);
                    bs.Customer = kisi;
                    bs.CustomersAdress = _customeradresservice.getir(firma.Id, (int)siparis[i].CustomerAddressId);
                    bs.Status = _statusservice.Get((int)siparis[i].StatusId);
    
                    var orderproducturunleri = _orderproductservice.Getorder(siparis[i].Id);
                    MenuNames menuler = new MenuNames();
                    List<MenuNames> menulerim = new List<MenuNames>();
                    DiscountCoupon indirim = new DiscountCoupon();
                    if(siparis[i].DiscountCouponId!=null)
                    {
                        var indirimbilgi = _discountcouponservice.Get((int)siparis[i].DiscountCouponId);
                        bs.DiscountCoupon = indirimbilgi;
                        indirim = indirimbilgi;
                    }
                   

                    for (int k = 0; k < orderproducturunleri.Count(); k++)
                    {

                        
                        
                        if (orderproducturunleri[k].OrderId == siparis[i].Id)
                        {
                            double menufiyat=0;
                            double kampanyafiyati=0;
                            double urunfiyat=0;
                          
                            if (orderproducturunleri[k].MenuNamesId!=null && orderproducturunleri[k].CampaignProductId == null && orderproducturunleri[k].ProductId == null)
                            {
                                var menuname = _menunamesservice.Get((int)orderproducturunleri[k].MenuNamesId);
                                menufiyat = Convert.ToDouble(menuname.Price);
                                kampanyafiyati = 0;
                                urunfiyat = 0;
                            }
                            if (orderproducturunleri[k].MenuNamesId == null && orderproducturunleri[k].CampaignProductId != null && orderproducturunleri[k].ProductId == null)
                            {
                                menufiyat = 0;
                                urunfiyat = 0;
                                var kampanya = _campaignproductnameservice.Get((int)orderproducturunleri[k].CampaignProductId);
                                kampanyafiyati = Convert.ToDouble(kampanya.Price);
                                

                            }
                            if (orderproducturunleri[k].MenuNamesId == null && orderproducturunleri[k].CampaignProductId == null && orderproducturunleri[k].ProductId != null)
                            {
                                menufiyat = 0;
                                kampanyafiyati = 0;
                                var productbilgi = _productservice.Get((int)orderproducturunleri[k].ProductId);
                                urunfiyat = Convert.ToDouble(productbilgi.Price);

                            }
                            if (orderproducturunleri[k].MenuNamesId != null && orderproducturunleri[k].CampaignProductId != null && orderproducturunleri[k].ProductId == null)
                            {
                                var menuname = _menunamesservice.Get((int)orderproducturunleri[k].MenuNamesId);
                                menufiyat = Convert.ToDouble(menuname.Price);
                                var kampanya = _campaignproductnameservice.Get((int)orderproducturunleri[k].CampaignProductId);
                                kampanyafiyati = Convert.ToDouble(kampanya.Price);
                                urunfiyat = 0;

                            }
                            if (orderproducturunleri[k].MenuNamesId != null && orderproducturunleri[k].CampaignProductId == null && orderproducturunleri[k].ProductId != null)
                            {
                                var menuname = _menunamesservice.Get((int)orderproducturunleri[k].MenuNamesId);
                                menufiyat = Convert.ToDouble(menuname.Price);
                                kampanyafiyati = 0;
                                var productbilgi = _productservice.Get((int)orderproducturunleri[k].ProductId);
                                urunfiyat = Convert.ToDouble(productbilgi.Price);

                            }
                            if (orderproducturunleri[k].MenuNamesId == null && orderproducturunleri[k].CampaignProductId != null && orderproducturunleri[k].ProductId != null)
                            {
                                menufiyat = 0;
                                var kampanya = _campaignproductnameservice.Get((int)orderproducturunleri[k].CampaignProductId);
                                kampanyafiyati = Convert.ToDouble(kampanya.Price);
                                var productbilgi = _productservice.Get((int)orderproducturunleri[k].ProductId);
                                urunfiyat = Convert.ToDouble(productbilgi.Price);

                            }
                            if (orderproducturunleri[k].MenuNamesId != null && orderproducturunleri[k].CampaignProductId != null && orderproducturunleri[k].ProductId != null)
                            {
                                var menuname = _menunamesservice.Get((int)orderproducturunleri[k].MenuNamesId);
                                menufiyat = Convert.ToDouble(menuname.Price);
                                var kampanya = _campaignproductnameservice.Get((int)orderproducturunleri[k].CampaignProductId);
                                kampanyafiyati = Convert.ToDouble(kampanya.Price);
                                var productbilgi = _productservice.Get((int)orderproducturunleri[k].ProductId);
                                urunfiyat = Convert.ToDouble(productbilgi.Price);

                            }
                         
                            toplamtutar = toplamtutar +(menufiyat * Convert.ToDouble(orderproducturunleri[k].MenusQuanty)) +
                                Convert.ToDouble(kampanyafiyati *Convert.ToDouble(orderproducturunleri[k].CampaignProductQuanty)) +
                                Convert.ToDouble(urunfiyat * Convert.ToDouble(orderproducturunleri[k].ProductQuanty));
                            indirimlitutar = toplamtutar -  Convert.ToDouble(indirim.DiscountCouponQuantity);
                            
                         

                        }
                        bs.siparistutari = toplamtutar;
                        bs.siparisindirimlitutari = indirimlitutar;
                        bs.OrderProduct.Add(orderproducturunleri[k]);
                    }
                    model.Siparisislemleri.Add(bs);
                    toplamtutar = 0;
                    indirimlitutar = 0;
                }
                return View(model);
            }  
                
        }
        public ActionResult Yorumlar()
        {

            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                List<Comment> yorumlar = new List<Comment>();
                var yorum = _commentservice.Getir(firma.Id);
                string dosyayolu = @"~\Areas\Admin\Content\hard.txt";
                StreamReader oku = new StreamReader(Server.MapPath("~/Areas/Admin/Content/hard.txt"));
                string[] dizi = new string[13];
                string[] dizi2 = new string[13];
                ArrayList elemanlar = new ArrayList();
                ArrayList yorumdizi = new ArrayList();
                string satirlar;
                int say=0;
                while ((satirlar = oku.ReadLine()) != null)
                {
                   
                    dizi = satirlar.Split(',');
                    int diziuzunluk = dizi.Count();
                    Array.Resize(ref dizi, diziuzunluk);
               
                    for (int i = 0; i < dizi.Count(); i++)
                    {
                        elemanlar.Add(dizi[i]);
                    }
                  
                    say++;
                }
                for (int k = 0; k < yorum.Count(); k++)
                {
                    dizi2 = yorum[k].Comments.Split(' ');
                    int diziuzunluk = dizi2.Count();
                    Array.Resize(ref dizi, diziuzunluk);
                    for (int j = 0; j <dizi2.Count() ; j++)
                    {
                        yorumdizi.Add(dizi2[j]);
                    }
                    for (int m = 0; m <yorumdizi.Count; m++)
                    {
                        for (int i = 0; i < elemanlar.Count; i++)
                        {
                            if (elemanlar[i].Equals(yorumdizi[m])==true)
                            {
                                Comment yorumum = new Comment();
                                yorumum.Id = yorum[k].Id;
                                yorumum = yorum[k];
                                yorumum.CommentConfirm = false;
                                _commentservice.Update(yorumum);
                            }
                        }
                    }
                    yorumdizi.Clear();
                    
                }


                oku.Close();
               return View(yorum);
            }
        }
        public ActionResult EncokSatanlar()
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                var siparisurunleri = _orderproductservice.GetAll();

                EncoksatisModel satis = new EncoksatisModel();
                satis.Productlist = new List<Product>();
                satis.MenuNameslist = new List<MenuNames>();
                satis.CampaignPNamelist = new List<CampaignPName>();
                satis.ProductSayisi = new List<int>();
                satis.MenuNameSayisi = new List<int>();
                satis.KampanyaSayisi = new List<int>();
                satis.productresmilist = new List<string>();
                satis.menuresmilist = new List<string>();
                satis.kampanyaresmi = new List<string>();
                

                var product = siparisurunleri.GroupBy(x => x.ProductId)
                      .Select(y => new {
                          Id = y.Key,
                          Quantity = y.Sum(x => x.ProductQuanty)
                      });
                var productsirala = product.OrderByDescending(x => x.Quantity);
                foreach (var item in productsirala)
                {
                    if (item.Id != null)
                    {
                        var urun = _productservice.Get((int)item.Id);
                        satis.Productlist.Add(urun);
                            var urunresim = _productimageservice.Getproduct(urun.Id);
                             if(urunresim.Resimler!=null)
                             {
                            var base64 = Convert.ToBase64String(urunresim.Resimler);
                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                            satis.productresmilist.Add(imagesrc);
                            }
                            if (urunresim.Resimler == null)
                            {
                                satis.productresmilist.Add(null);
                            }
                        satis.ProductSayisi.Add((int)item.Quantity);
                       
                    }
                }

                var menuname = siparisurunleri.GroupBy(x => x.MenuNamesId)
                    .Select(y => new {
                        Id = y.Key,
                        Quantity = y.Sum(x => x.MenusQuanty)
                    });
                var menunamesirala = menuname.OrderByDescending(x => x.Quantity);
                foreach (var item in menunamesirala)
                {
                    if (item.Id != null)
                    {
                        var menuler = _menunamesservice.Get((int)item.Id);
                        satis.MenuNameslist.Add(menuler);
                        if (menuler.MenuResim != null)
                        {
                            
                            var base64 = Convert.ToBase64String(menuler.MenuResim);
                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                            satis.menuresmilist.Add(imagesrc);
                        }
                        if(menuler.MenuResim==null)
                        {
                            satis.menuresmilist.Add(null);
                        }
                        satis.MenuNameSayisi.Add((int)item.Quantity);
                    }
                }
                var kampanyaname = siparisurunleri.GroupBy(x => x.CampaignProductId)
               .Select(y => new {
                   Id = y.Key,
                   Quantity = y.Sum(x => x.CampaignProductQuanty)
               });
              var kampanyanamesirala = kampanyaname.OrderByDescending(x => x.Quantity);
                foreach (var item in kampanyanamesirala)
                {
                    if (item.Id != null)
                    {
                        var kampanyalar = _campaignproductnameservice.Get((int)item.Id);
                        satis.CampaignPNamelist.Add(kampanyalar);
                        if (kampanyalar.CampaignResim != null)
                        {
                            var base64 = Convert.ToBase64String(kampanyalar.CampaignResim);
                            var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                            satis.kampanyaresmi.Add(imagesrc);
                        }
                        if(kampanyalar.CampaignResim==null)
                        {
                            satis.kampanyaresmi.Add(null);
                        }
                        
                        satis.KampanyaSayisi.Add((int)item.Quantity);
                    }
                }
                return View(satis);
            }
        }
      
      
        public ActionResult IndırımKuponlari(int sayfa=1)
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                var indirimkuponlari = _discountcouponservice.Getir(firma.Id).ToPagedList(sayfa, 10);
                return View(indirimkuponlari);
            
               
            }
        }
        
        public ActionResult KuponSil(int vid)
        {
            var silinecek = _discountcouponservice.Get(vid);
            _discountcouponservice.Delete(silinecek);
            return RedirectToAction("IndırımKuponlari");
        }
        public ActionResult KuponEkle()
        {

         
            return View();
        }
        [HttpPost]
        public ActionResult KuponEkleme(DiscountCoupon kupon)
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                DiscountCoupon veri = new DiscountCoupon();
                
                veri = kupon;
                veri.CompaniesId = firma.Id;
                _discountcouponservice.Add(veri);
                return RedirectToAction("IndırımKuponlari");
            }
           
        }

        public ActionResult IndirimDuzenle(int Id)
        {
            var kupon = _discountcouponservice.Get(Id);

            return View(kupon);
        }
        [HttpPost]
        public ActionResult KuponDuzenle(DiscountCoupon kupon)
        {
            var indirimkuponu=_discountcouponservice.Get(kupon.Id);
            indirimkuponu.Id = kupon.Id;
            indirimkuponu = kupon;
            _discountcouponservice.Update(indirimkuponu);
            return RedirectToAction("IndırımKuponlari");
        }
        public ActionResult IndirimSil(int Id)
        {

            return View();
        }
        public ActionResult SifremiUnuttum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifremiUnuttum(string emailadresi)
        {
            var kullanici = _managertableservice.Getir(emailadresi);
            if (kullanici != null)
            {
                if (kullanici.AuthorityId == 1)
                {
                    string serinumara = Guid.NewGuid().ToString();
                    MailMessage mailim = new MailMessage();
                    mailim.To.Add(emailadresi);
                    var tarih = DateTime.Now;
                    var gecerliolduguzmn = DateTime.Now.AddDays(1);
                    _sifredegisiklikservice.Add(new SifreDegisiklik { guidimiz = serinumara, active = true, kullaniciadi = kullanici.Mname, ilkkayittarihi = DateTime.Now, gecerliliksuresi = gecerliolduguzmn });
                    mailim.From = new MailAddress("zehrakarahan95@gmail.com");
                    var url = "https://" + Request.Url.Authority + "/Admin/Anasayfa/SifreResetle?guid=" + serinumara;
                    //var urlrenkli="<h3>"+url+"</h3>"
                    mailim.Subject = "Yemek Sepeti Şifre Değişikliği";

                    mailim.Body = "<h1 style='margin-left:200px; color:black;'>Yemek Sepeti </h1> " + "  " + "<h3 style='color=black;'> Merhaba Sevgili " + " " + kullanici.Mname.ToUpper() + " " + " Şifre Bilginizi Sıfırlamak için Linke Tıklayınız....</h3>" +
                        url;
                    mailim.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Credentials = new NetworkCredential("zehrakarahan95@gmail.com", "141220034.6928");
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(mailim);
                        TempData["mailbilgi"] = "Şifrenizi sıfırlamak için gerekli link e-posta adresinize gönderilmiştir. ";


                    }
                    catch (Exception ex)
                    {

                        TempData["teknikagriza"] = "Teknik bir arızadan dolayı mail gönderilemedi.";
                    }
                }
                else
                {
                    TempData["admindegil"] = "Bu işlem için Admin Olmanız Gerekiyor";
                    return RedirectToAction("SifremiUnuttum");
                }
            }
            else
            {
                TempData["mailkayitlidegil"] = "Bu mail Adresine ait Kişi Yoktur";
                return RedirectToAction("SifremiUnuttum");
            }
            return View();
        }
        [HttpPost]
        public ActionResult SifreSifirlama(AdminKullaniciSifre model)
        {
            var sifre = new ToPasswordRepository().Md5(model.yenisifre);
            ManagerTable verim = new ManagerTable();
            verim.Mname = model.ManagerTable.Mname;
            verim.EmailAdresi = model.ManagerTable.EmailAdresi;
            verim.CompaniesId = model.ManagerTable.CompaniesId;
            verim.AuthorityId = model.ManagerTable.AuthorityId;
            verim.Password = model.yenisifre;
            var kullan = _managertableservice.Get(model.ManagerTable.Mname,model.ManagerTable.Password);
            kullan.AuthorityId = verim.AuthorityId;
            kullan.CompaniesId = verim.CompaniesId;
            kullan.EmailAdresi = verim.EmailAdresi;
            kullan.Mname = verim.Mname;
            kullan.Password = verim.Password;
            kullan.Id = kullan.Id;
            
            _managertableservice.Update(kullan);

            TempData["sifresifirla"] = "  Şifre sıfırlama işlemi gerçekleşmiştir...";
            return RedirectToAction("KullaniciGiris");
        }
        public ActionResult SifreResetle(string guid)
        {

            var kisimiz = _sifredegisiklikservice.Get(guid);
            var kullaniciadi = kisimiz.kullaniciadi;

            if (kisimiz.gecerliliksuresi > DateTime.Now)
            {
                if (kisimiz.active == true)
                {
                    SifreDegisiklik yenidurum = new SifreDegisiklik();

                    yenidurum.songuncellenmetarihi = DateTime.Now;
                    yenidurum.kullaniciadi = kisimiz.kullaniciadi;
                    yenidurum.ilkkayittarihi = kisimiz.ilkkayittarihi;
                    yenidurum.active = false;
                    yenidurum.guidimiz = kisimiz.guidimiz;
                    yenidurum.Id = kisimiz.Id;
                    _sifredegisiklikservice.Update(yenidurum);
                    var kullaniciadimiz = kullaniciadi;
                    var kidi = _managertableservice.Geti(kullaniciadimiz);
                    AdminKullaniciSifre model = new AdminKullaniciSifre();
                    model.ManagerTable = kidi;
                    return View(model);
                }
                else
                {
                    TempData["guidgecersiz"] = "Linkiniz Artık Geçerli Değildir...";
                    return RedirectToAction("SifremiUnuttum");
                }

            }
            else
            {
                ViewBag.gecersizguid = "Bu Kullanıcı Link Geçersiz Şuan!!!";
                TempData["guidsuresidoldu"] = "Malesef Kullanım süresi Doldu...";
                return RedirectToAction("SifremiUnuttum");
            }
        }
        public ActionResult KullaniciGiris()
        {
            GirisBilgilerim veri = new GirisBilgilerim();
            return View(veri);
        }
        [HttpPost]
        public ActionResult KullaniciGiris(GirisBilgilerim bilgi)
        {
            var sifre = new ToPasswordRepository().Md5(bilgi.sifre);
            var kullanici = _managertableservice.Get(bilgi.KullaniciAdi,sifre);
            
            if (kullanici!=null)
            {
                if(kullanici.AuthorityId==1)
                {
                    var firmabilgisi = _companiesservice.Get((int)kullanici.CompaniesId);
                    Session["KullaniciAdi"] = kullanici.Mname;
                    Session["firmaadi"] = firmabilgisi.CompanyName;
                    return RedirectToAction("Index");
                }
                else
                {     
                    TempData["kullaniciadmindegil"] = "Üzgünüz Admin Değilsiniz";
                    return RedirectToAction("KullaniciGiris");
                }
            }
            else
            {
                TempData["Bukullaniciyok"] = "Mevcutta Böyle Bir Kullanıcı Mevcut Değil";
                return RedirectToAction("KullaniciGiris");
            }
           
        }
        public ActionResult CikisYap()
        {
            Session.Remove("KullaniciAdi");
            Session.Remove("firmaadi");
            return RedirectToAction("KullaniciGiris");
        }
        
        public ActionResult PromosyonBilgileri()
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var verim = TempData["basarili"];
                var menuname = TempData["menubasarili"];
                var kampanyaname = TempData["kampanyabasarili"];
                TempData["kampanyabasrli"] = kampanyaname;
                TempData["menubasarli"] = menuname;
                TempData["basarilikayit"] = verim;
                return View();
            }
        }
        public ActionResult UrunBazliPro(int id)
        {
            TempData["degerim"] = id;
            return RedirectToAction("UrunBazliPromosyon");
        }
        public ActionResult SayfaSifirla()
        {
            return RedirectToAction("UrunBazliPromosyon");
        }
        [HttpPost]
        public ActionResult UrunBazliPromosyon(PromosyonveProduct urunlerbilgi)
        {
            /* var urunoran= urunlerbilgi.Promosyon.PromosyonQuantity.ToCharArray();
             var  asciikodurun = System.Text.Encoding.UTF8.GetBytes(urunoran);*/
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                Promosyon promos = new Promosyon();
                promos = urunlerbilgi.Promosyon;
                promos.CompaniesId = firma.Id;
                _promosyonservice.Add(promos);
                var tumelemanlar = _promosyonservice.GetAll();
                var soneleman = tumelemanlar.Last();
                var product = _productservice.Get(urunlerbilgi.Product.Id);
                Product urunler = new Product();
                urunler.Id = product.Id;
                urunler = product;
                urunler.PromosyonId = soneleman.Id;
                _productservice.Update(urunler);
                TempData["basarili"] = "urun kaydedildi";
                return RedirectToAction("PromosyonBilgileri");
            }
        }
        public ActionResult UrunBazliPromosyon()
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                var promosyonlurunler = _promosyonservice.Getirliste(firma.Id);
                var urunler = _productservice.Geturun(firma.Id);
                PromosyonveProduct veri = new PromosyonveProduct();
                veri.Productlist = new List<Product>();
                veri.Promosyonlist = new List<Promosyon>();
                veri.UrunResmi = new List<string>();
                DateTime bugun = DateTime.Now;
                for (int i = 0; i < promosyonlurunler.Count(); i++)
                {
                    if(bugun>promosyonlurunler[i].PromasyonBitisTarih)
                    {
                        var urunpromosyon = _productservice.Getpromosyon(promosyonlurunler[i].Id, firma.Id);
                        Product urundegis = new Product();
                        urundegis.Id = urunpromosyon.Id;
                        urundegis = urunpromosyon;
                        urundegis.PromosyonId = null;
                        _promosyonservice.Delete(promosyonlurunler[i]);
                    }
                }
                var bakiyeid = TempData["degerim"];
                if (bakiyeid == null)
                {
                    for (int i = 0; i < urunler.Count(); i++)
                    {

                        if (urunler[i].PromosyonId == null)
                        {
                            veri.Productlist.Add(urunler[i]);
                        }
                        if (urunler[i].ProductImageId != null)
                        {
                            var urunresmi = _productimageservice.Getir((int)firma.Id, (int)urunler[i].ProductImageId);
                            if (urunresmi.Resimler != null)
                            {
                                var base64 = Convert.ToBase64String(urunresmi.Resimler);
                                var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                veri.UrunResmi.Add(imagesrc);
                            }
                            if (urunresmi.Resimler == null)
                            {
                                veri.UrunResmi.Add(null);
                            }
                        }
                    }
                   
                }
                if (bakiyeid!=null)
                {
                    for (int i = 0; i < urunler.Count(); i++)
                    {

                        if (urunler[i].PromosyonId == null)
                        {
                            veri.Productlist.Add(urunler[i]);
                        }
                        if (urunler[i].ProductImageId != null)
                        {
                            var urunresmi = _productimageservice.Getir((int)firma.Id, (int)urunler[i].ProductImageId);
                            if (urunresmi.Resimler != null)
                            {
                                var base64 = Convert.ToBase64String(urunresmi.Resimler);
                                var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                                veri.UrunResmi.Add(imagesrc);
                            }
                            if (urunresmi.Resimler == null)
                            {
                                veri.UrunResmi.Add(null);
                            }
                        }
                    }
                    var urunum = _productservice.Get((int)bakiyeid);
                    veri.Product = urunum;
                  
                }
               
                return View(veri);
            }
        }


        [HttpPost]
        public ActionResult MenuBazliPromosyon(PromosyonveMenu promosyonmenu)
        {
            /* var urunoran= urunlerbilgi.Promosyon.PromosyonQuantity.ToCharArray();
             var  asciikodurun = System.Text.Encoding.UTF8.GetBytes(urunoran);*/
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                Promosyon promos = new Promosyon();
                promos = promosyonmenu.Promosyon;
                promos.CompaniesId = firma.Id;
                _promosyonservice.Add(promos);
                var tumelemanlar = _promosyonservice.GetAll();
                var soneleman = tumelemanlar.Last();
                var menuname = _menunamesservice.Get(promosyonmenu.MenuNames.Id);
                MenuNames menuler = new MenuNames();
                menuler.Id = menuname.Id;
                menuler = menuname;
                menuler.PromosyonId = soneleman.Id;
                _menunamesservice.Update(menuler);
                TempData["menubasarili"] = "menu kaydedildi";
                return RedirectToAction("PromosyonBilgileri");
            }
        }
        public ActionResult MenuSifirla()
        {
            return RedirectToAction("MenuBazliPromosyon");
        }
        public ActionResult MenuBazliPro(int id)
        {
            TempData["menuid"] = id;
            return RedirectToAction("MenuBazliPromosyon");
        }
        public ActionResult MenuBazliPromosyon()
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                var promosyonlurunler = _promosyonservice.Getirliste(firma.Id);
                var firmaurun = _menunamesservice.Getirliste(firma.Id);
                PromosyonveMenu veri = new PromosyonveMenu();
                veri.MenuNameslist = new List<MenuNames>();
                veri.Promosyonlist = new List<Promosyon>();
                DateTime bugun = DateTime.Now;
                for (int i = 0; i < promosyonlurunler.Count(); i++)
                {
                    if (bugun > promosyonlurunler[i].PromasyonBitisTarih)
                    {
                        var urunpromosyon =_menunamesservice.GetPromosyon(promosyonlurunler[i].Id, firma.Id);
                        MenuNames urundegis = new MenuNames();
                        urundegis.Id = urunpromosyon.Id;
                        urundegis = urunpromosyon;
                        urundegis.PromosyonId = null;
                        _promosyonservice.Delete(promosyonlurunler[i]);
                    }
                }
                var bakiyeid = TempData["menuid"];
                if (bakiyeid == null)
                {
                    for (int i = 0; i < firmaurun.Count(); i++)
                    {

                        if (firmaurun[i].PromosyonId == null)
                        {
                            veri.MenuNameslist.Add(firmaurun[i]);
                        }
                    }

                }
                if (bakiyeid != null)
                {
                    for (int i = 0; i < firmaurun.Count(); i++)
                    {

                        if (firmaurun[i].PromosyonId == null)
                        {
                            veri.MenuNameslist.Add(firmaurun[i]);
                        }


                    }
                    var urunum = _menunamesservice.Get((int)bakiyeid);
                    veri.MenuNames = urunum;
                 
                }
                return View(veri);
            }
        }
        public ActionResult KampanyaSifirla()
        {
            return RedirectToAction("KampanyaBazliPromosyon");
        }
        public ActionResult KampanyaBazliPro(int id)
        {
            TempData["kampanyaid"] = id;
            return RedirectToAction("KampanyaBazliPromosyon");
        }
        public ActionResult KampanyaBazliPromosyon()
        {
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                var promosyonlurunler = _promosyonservice.Getirliste(firma.Id);
                var firmaurun = _campaignproductnameservice.Getirliste(firma.Id);
                PromosyonveKampanya veri = new PromosyonveKampanya();
                veri.CampaignPNamelist = new List<CampaignPName>();
                veri.Promosyonlist = new List<Promosyon>();
                DateTime bugun = DateTime.Now;
                for (int i = 0; i < promosyonlurunler.Count(); i++)
                {
                    if (bugun > promosyonlurunler[i].PromasyonBitisTarih)
                    {
                        var urunpromosyon = _menunamesservice.GetPromosyon(promosyonlurunler[i].Id, firma.Id);
                        MenuNames urundegis = new MenuNames();
                        urundegis.Id = urunpromosyon.Id;
                        urundegis = urunpromosyon;
                        urundegis.PromosyonId = null;
                        _promosyonservice.Delete(promosyonlurunler[i]);
                    }
                }
                var bakiyeid = TempData["kampanyaid"];
                if (bakiyeid == null)
                {
                    for (int i = 0; i < firmaurun.Count(); i++)
                    {

                        if (firmaurun[i].PromosyonId == null)
                        {
                            veri.CampaignPNamelist.Add(firmaurun[i]);
                        }
                    }

                }
                if (bakiyeid != null)
                {
                    for (int i = 0; i < firmaurun.Count(); i++)
                    {

                        if (firmaurun[i].PromosyonId == null)
                        {
                            veri.CampaignPNamelist.Add(firmaurun[i]);
                        }


                    }
                    var urunum = _campaignproductnameservice.Get((int)bakiyeid);
                    veri.CampaignPName = urunum;

                }
                return View(veri);
            }
        }
        [HttpPost]
        public ActionResult KampanyaBazliPromosyon(PromosyonveKampanya promosyonkampanya)
        {
            /* var urunoran= urunlerbilgi.Promosyon.PromosyonQuantity.ToCharArray();
             var  asciikodurun = System.Text.Encoding.UTF8.GetBytes(urunoran);*/
            if (Session["firmaadi"] == null)
            {
                return RedirectToAction("KullaniciGiris");
            }
            else
            {
                var girisyapan = Session["KullaniciAdi"];
                var firmaadi = Session["firmaadi"].ToString();
                var firma = _companiesservice.Get(firmaadi);
                Promosyon promos = new Promosyon();
                promos = promosyonkampanya.Promosyon;
                promos.CompaniesId = firma.Id;
                _promosyonservice.Add(promos);
                var tumelemanlar = _promosyonservice.GetAll();
                var soneleman = tumelemanlar.Last();
                var kampanya = _campaignproductnameservice.Get(promosyonkampanya.CampaignPName.Id);
                CampaignPName kampnyalar = new CampaignPName();
                kampnyalar.Id = kampanya.Id;
                kampnyalar = kampanya;
                kampnyalar.PromosyonId = soneleman.Id;
                _campaignproductnameservice.Update(kampnyalar);
                TempData["kampanyabasarili"] = "kampanya kaydedildi";
                return RedirectToAction("PromosyonBilgileri");
            }
        }

    }
}