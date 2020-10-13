using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using YemekSepetiUygulamasi.Bll.Abstract;
using YemekSepetiUygulamasi.Entity.EntityTable;
using Facebook;
using System.Web.Security;
using YemekSepetiUygulamasi.WebUI.Models;
using YemekSepetiUygulamasi.Bll;
using System.Net.Mail;

namespace YemekSepetiUygulamasi.WebUI.Controllers
{
    public class HomeController : Controller
    {
        ISliderService _sliderService;
        ICityService _cityservice;
        ICustomerService _customerservice;
        ICustomersAdressService _customeradresservice;
        ISifreDegisiklikService _sifredegisiklikservice;
        ICategoryService _categoryservice;
        ICompaniesService _companieservice;
        public HomeController(ICompaniesService companieservice,ICategoryService categoryservice,ISifreDegisiklikService sifredegisiklikservice, ISliderService sliderService,ICityService cityservice,ICustomerService customerservice,ICustomersAdressService customeradresservice)
        {
            this._sliderService = sliderService;
            this._cityservice = cityservice;
            this._customerservice = customerservice;
            this._customeradresservice = customeradresservice;
            this._sifredegisiklikservice = sifredegisiklikservice;
            this._categoryservice = categoryservice;
            this._companieservice = companieservice;
          
        }
        public ActionResult Index()
        {
            var veriler= _sliderService.GetAll();
            List<string> resimler = new List<string>();
            for (int i = 0; i < veriler.Count(); i++)
            {
                var base64 = Convert.ToBase64String(veriler[i].SliderFoto);
                var imagesrc = string.Format("data:image/gif;base64,{0}", base64);
                resimler.Add(imagesrc);
            }
            ViewBag.resimler = resimler;
         
            return View();

        }
        public ActionResult Sehirler()
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
            var sehirler = _cityservice.GetAll();
            TempData["sehirler"] = sehirler;
            return View();
        }
        public ActionResult Uyeol()
        {
            UyeBilgileri bilgi = new UyeBilgileri();
            return View(bilgi);
        }
        [HttpPost]
        public ActionResult UyeOl(UyeBilgileri bilgi)
        {
            var sifre = new ToPasswordRepository().Md5(bilgi.parola);
           
           
            _customeradresservice.Add(new CustomersAdress { CustomerAdress = bilgi.adres });
            var sonkisi=_customeradresservice.GetAll().LastOrDefault();
            _customerservice.Add(new Customer
            {
                Cname = bilgi.isim,
                UserName = bilgi.soyisim,
                CountryName = bilgi.ulke,
                CityName = bilgi.il,
                CountyName = bilgi.ilce,
                NeighborhoodName = bilgi.semt,
                Password = sifre,
                EmailAdress = bilgi.Email,
                CustomersAdressId=sonkisi.Id
            });

            return RedirectToAction("GirisYap");
        }
        public ActionResult GirisYap()
        {
            var bilgi = TempData["bilgiler"];
            TempData["kullanicibilgileri"] = bilgi;
            GirisBilgileri veri = new GirisBilgileri();
            return View(veri);
        }
        [HttpPost]
        public ActionResult GirisYap(GirisBilgileri bilgi)
        {
            var anasayfa=_customerservice.KullaniciGiris(bilgi.isimveyaEmail, bilgi.parola);
            Ilcevesemt veriler = (Ilcevesemt)Session["bilgiler"];
            var sehirid = Session["sehirid"];
            if (veriler==null)
            {
                if (anasayfa != null)
                {
                    Session["adsoyadi"] = anasayfa.Cname;
                    Session["kullaniciadi"] = anasayfa.UserName;
                    return RedirectToAction("Index", "Anasayfalar", new { cityId = sehirid });
                }
                else
                {
                    TempData["bilgiler"] = "Kullanıcı Adı veya Şifre Hatalı";
                    return RedirectToAction("GirisYap");
                }
            }
            else
            {
                if (anasayfa != null)
                {
                    Session["adsoyadi"] = anasayfa.Cname;
                    Session["kullaniciadi"] = anasayfa.UserName;
                    return RedirectToAction("AramaSonuclari", "Anasayfalar", veriler);
                }
                else
                {
                    TempData["bilgiler"] = "Kullanıcı Adı veya Şifre Hatalı";
                    return RedirectToAction("GirisYap");
                }
            }
          
          
            
        }
        public ActionResult SifremiUnuttum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifremiUnuttum(string EmailAdresi)
        {
            var sonuc=_customerservice.EmailDogrula(EmailAdresi);
            if(sonuc!=null)
            {
                string serinumara = Guid.NewGuid().ToString();
                MailMessage mailim = new MailMessage();
                mailim.To.Add(EmailAdresi);
                var tarih = DateTime.Now;
                var gecerliolduguzmn = DateTime.Now.AddDays(1);
                _sifredegisiklikservice.Add(new SifreDegisiklik { guidimiz = serinumara, active = true, kullaniciadi = sonuc.UserName, ilkkayittarihi = DateTime.Now, gecerliliksuresi = gecerliolduguzmn });
                mailim.From = new MailAddress("zehrakarahan95@gmail.com");
                var url = "https://" + Request.Url.Authority + "/Anasayfa/SifreResetle?guid=" + serinumara;
                //var urlrenkli="<h3>"+url+"</h3>"
                mailim.Subject = "Yemek Sepeti Şifre Değişikliği";

                mailim.Body = "<h1 style='margin-left:200px; color:black;'>Yemek Sepeti </h1> " + "  " + "<h3 style='color=black;'> Merhaba Sevgili " + " " + sonuc.Cname.ToUpper() + " " + " Şifre Bilginizi Sıfırlamak için Linke Tıklayınız....</h3>" +
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
                TempData["mailgonderilmedi"] = "Bu Mail Adresiyle Uyuşan kullanıcı mevcut değildir.";
            }
            return View();
        }
        [HttpPost]
        public ActionResult SifreSifirlama(KullaniciModel model)
        {
            var sifre = new ToPasswordRepository().Md5(model.Yenisifre);
            Customer verim = new Customer();
            verim.Cname = model.Customer.Cname;
            verim.UserName = model.Customer.UserName;
            verim.Password = model.Yenisifre;
            var kullan = _customerservice.GetKullaniciKullaniciName(model.Customer.UserName);
            kullan.Cname = model.Customer.Cname;
            kullan.Password = sifre;
            kullan.UserName = model.Customer.UserName;
            kullan.CityName = model.Customer.CityName;
            kullan.CountryName = model.Customer.CountryName;
            kullan.CountyName = model.Customer.CountyName;
            kullan.NeighborhoodName = model.Customer.NeighborhoodName;
            kullan.CustomersAdressId = model.Customer.CustomersAdressId;
            _customerservice.Update(kullan);

            TempData["sifresifirla"] = "  Şifre sıfırlama işlemi gerçekleşmiştir...";
            return RedirectToAction("GirisYap");
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
                    var kidi = _customerservice.GetKullaniciKullaniciName(kullaniciadimiz);
                    KullaniciModel model = new KullaniciModel();
                    model.Customer = kidi;
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
        public ActionResult Resimler()
        {
            var veriler = _sliderService.GetAll();
           
            return View();
        }
        public ActionResult ResimEkle()
        {   
            return View();
        }
        public ActionResult CategoriResimEkle()
        {
            CategoryBilgileri deneme = new CategoryBilgileri();
            deneme.Category = null;
            deneme.resimbilgi = null;
            return View(deneme);
        }
        [HttpPost]
        public ActionResult UrunEkle(CategoryBilgileri bilgiler)
        {
            Category kayit = new Category(); 
            kayit = bilgiler.Category;
            kayit.CompaniesId = 1;
            kayit.CResim = new byte[bilgiler.resimbilgi.ContentLength];
            bilgiler.resimbilgi.InputStream.Read(kayit.CResim, 0, bilgiler.resimbilgi.ContentLength);
            _categoryservice.Add(kayit);
            return RedirectToAction("CategoriResimEkle");
        }
        public ActionResult Slider()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveImages(HttpPostedFileBase SliderFoto)
        {
            Slider kayit = new Slider();
            kayit.SliderFoto = new byte[SliderFoto.ContentLength];
            SliderFoto.InputStream.Read(kayit.SliderFoto, 0, SliderFoto.ContentLength);
            _sliderService.Add(kayit);
            return RedirectToAction("Index");
        }
        public ActionResult firmaresim()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult firmaresim(HttpPostedFileBase resim)
        {
            Companies kayit = new Companies();

           
            var firma=_companieservice.Get(3);
            kayit = firma;
            kayit.CompanyLogo =new byte[resim.ContentLength];
            resim.InputStream.Read(kayit.CompanyLogo, 0, resim.ContentLength);
            _companieservice.Update(kayit);
            return RedirectToAction("firmaresim");
        }
        public ActionResult CikisYap()
        {
            Session.Remove("adsoyadi");
            Session.Remove("kullaniciadi");
            return RedirectToAction("Sehirler");
        }
        public ActionResult CikisYap2()
        {
            Session.Remove("adsoyadi");
            Session.Remove("kullaniciadi");
            Ilcevesemt veriler = (Ilcevesemt)Session["bilgiler"];
            return RedirectToAction("AramaSonuclari","Anasayfalar", veriler);
        }
    }
}