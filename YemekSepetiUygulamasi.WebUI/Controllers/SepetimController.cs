using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YemekSepetiUygulamasi.Bll.Abstract;
using YemekSepetiUygulamasi.Entity.EntityTable;
using YemekSepetiUygulamasi.WebUI.Models;
using YemekSepetiUygulamasi.WebUI.ServiceRedis;

namespace YemekSepetiUygulamasi.WebUI.Controllers
{
    public class SepetimController : Controller
    {
        // GET: Sepetim
        ICityService _cityservice;
        ISliderService _sliderService;
        ICountyService _countyservice;
        ICompaniesService _companieservice;
        INeighborhoodService _neighborhoodservice;
        IProductService _productservice;
        ICategoryService _categoryservice;
        IMenuNamesService _menunameservice;
        ICommentService _commentservice;
        IMenusService _menuservice;
        private const string CacheName = "MyFirstCacheVariableInRedis";
        private readonly ICacheProvider _cacheProvider;
        public SepetimController(IMenusService menuservice,ICommentService commentservice, IMenuNamesService menunameservice, ICategoryService categoryservice, IProductService productservice, INeighborhoodService neighborhoodservice, ICompaniesService companieservice, ICountyService countyservice, ISliderService sliderService, ICityService cityservice)
        {
            this._sliderService = sliderService;
            this._cityservice = cityservice;
            this._countyservice = countyservice;
            this._companieservice = companieservice;
            this._neighborhoodservice = neighborhoodservice;
            this._productservice = productservice;
            this._categoryservice = categoryservice;
            this._menunameservice = menunameservice;
            this._commentservice = commentservice;
            this._menuservice = menuservice;
            _cacheProvider = new CacheProvider();
        }
       
        [HttpPost]
        public void DegistirTutar(int id, string urunadi, int yeniadet, int firmaid)
        {

            SepetItem sepetitem = new SepetItem();
            var urunler = _productservice.Getirbilgi(firmaid, urunadi, id);
            var menuler = _menunameservice.GetmenuBilgisi(firmaid, urunadi, id);
            if (urunler != null)
            {
                sepetitem.urunid = urunler.Id;
                sepetitem.urunadi = urunler.Pname;
                sepetitem.fiyati = (decimal)urunler.Price;
                sepetitem.adeti = yeniadet;
                sepetitem.firmaid = firmaid;

                Sepet sepet = new Sepet();
                sepet.SepetAdetDegistir(sepetitem);
            }
            if (menuler != null)
            {
                sepetitem.urunid = menuler.Id;
                sepetitem.urunadi = menuler.MenuName;
                sepetitem.fiyati = (decimal)menuler.Price;
                sepetitem.adeti = yeniadet;
                sepetitem.firmaid = firmaid;
                Sepet sepet = new Sepet();
                sepet.SepetAdetDegistir(sepetitem);
            }
        }
        public PartialViewResult MiniSepet2()
        {
            var sepet = (Sepet)HttpContext.Session["AktifSepet"];
            _cacheProvider.Set(CacheName, sepet);
            if (sepet != null)
            {
                if (sepet.Urunler.Count() > 0)
                {
                    if (HttpContext.Session["AktifSepet"] != null)
                    {
                        return PartialView((Sepet)HttpContext.Session["AktifSepet"]);
                    }
                    else
                    {
                        return PartialView();
                    }
                }
                if (sepet.Urunler.Count == 0)
                {
                    return PartialView();
                }
            }
            if (sepet == null)
            {
                return PartialView();
            }

            return PartialView();
        }
        public PartialViewResult MiniSepet()
        {
            var sepet = (Sepet)HttpContext.Session["AktifSepet"];
            _cacheProvider.Set(new Guid().ToString(), sepet);
            if (sepet != null)
            {
                if (sepet.Urunler.Count() > 0)
                {
                    if (HttpContext.Session["AktifSepet"] != null)
                    {
                        return PartialView((Sepet)HttpContext.Session["AktifSepet"]);
                    }
                    else
                    {
                        return PartialView();
                    }
                }
                if (sepet.Urunler.Count == 0)
                {
                    return PartialView();
                }
            }
            if (sepet==null)
            {
                return PartialView();
            }
             
            return PartialView();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void SepetBosalt()
        {
            HttpContext.Session["AktifSepet"] = null;

        }
        public ActionResult Firmabilgi(int id)
        {
            Firmabilgilistesi veri = new Firmabilgilistesi();
            veri.Menunamelistesi = new List<MenuNames>();
            veri.Menulistesi = new List<Menus>();
            veri.yorumlistesi = new List<Comment>();
            foreach (var item in _menunameservice.Getirliste(id))
            {
                veri.Menunamelistesi.Add(item);
            }
            veri.firmabilgileri = _companieservice.Get(id);
            var yorum = _commentservice.Getir(id);
            string dosyayolu = @"~\Areas\Admin\Content\hard.txt";
            StreamReader oku = new StreamReader(Server.MapPath("~/Areas/Admin/Content/hard.txt"));
            string[] dizi = new string[13];
            string[] dizi2 = new string[13];
            ArrayList elemanlar = new ArrayList();
            ArrayList yorumdizi = new ArrayList();
            string satirlar;
            int say = 0;
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
                for (int j = 0; j < dizi2.Count(); j++)
                {
                    yorumdizi.Add(dizi2[j]);
                }
                for (int m = 0; m < yorumdizi.Count; m++)
                {
                    for (int i = 0; i < elemanlar.Count; i++)
                    {
                        if (elemanlar[i].Equals(yorumdizi[m]) == true)
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
            foreach (var item in _commentservice.Getir(id))
            {
                if (item.CommentConfirm==true)
                {
                    veri.yorumlistesi.Add(item);
                }
                
            }
            foreach (var item in _menunameservice.Getirliste(id))
            {
                foreach (var item2 in _menuservice.Getirlistefirma(id))
                {
                    if (item.CompaniesId == item2.CompaniesId && item.Id == item2.MenuNamesId)
                    {
                        veri.Menulistesi.Add(item2);
                    }
                }
            }
            return View(veri);
        }
        [HttpPost]
        public void SepetUrunSil(int id, string urunadi, int adet, int firmaid)
        {
            SepetItem sepetitem = new SepetItem();
            var urunler = _productservice.Getirbilgi(firmaid, urunadi, id);
            var menuler = _menunameservice.GetmenuBilgisi(firmaid, urunadi, id);
            if (urunler != null)
            {
                sepetitem.urunid = urunler.Id;
                sepetitem.urunadi = urunler.Pname;
                sepetitem.fiyati = (decimal)urunler.Price;
                sepetitem.adeti = adet;
                sepetitem.firmaid = firmaid;

                Sepet sepet = new Sepet();
                sepet.UrunuSil(sepetitem);
            }
            if (menuler != null)
            {
                sepetitem.urunid = menuler.Id;
                sepetitem.urunadi = menuler.MenuName;
                sepetitem.fiyati = (decimal)menuler.Price;
                sepetitem.adeti = adet;
                sepetitem.firmaid = firmaid;
                Sepet sepet = new Sepet();
                sepet.UrunuSil(sepetitem);
            }
        }
        public void SepeteEkle(int id,string urunadi,int adet,int firmaid)
        {
           
            SepetItem sepetitem = new SepetItem();

            var urunler= _productservice.Getirbilgi(firmaid,urunadi,id);
            var menuler = _menunameservice.GetmenuBilgisi(firmaid,urunadi,id);
            if (urunler!=null)
            {
                sepetitem.urunid= urunler.Id;
                sepetitem.urunadi = urunler.Pname;
                sepetitem.fiyati =(decimal)urunler.Price;
                sepetitem.adeti = adet;
                sepetitem.firmaid = firmaid;
            
                Sepet sepet = new Sepet();
                sepet.SepeteEkle(sepetitem);
            }
            if (menuler!=null)
            {
                sepetitem.urunid = menuler.Id;
                sepetitem.urunadi = menuler.MenuName;
                sepetitem.fiyati = (decimal)menuler.Price;
                sepetitem.adeti = adet;
                sepetitem.firmaid = firmaid;
                Sepet sepet = new Sepet();
                sepet.SepeteEkle(sepetitem);
            }   
        
        }
    }
}