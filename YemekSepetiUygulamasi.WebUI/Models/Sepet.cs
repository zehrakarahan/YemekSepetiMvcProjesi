using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.WebUI.Models
{
    public class Sepet
    {
        public static Sepet AktifSepetmi
        {
            get
            {
                HttpContext ctx = HttpContext.Current;
                if (ctx.Session["AktifSepet"] == null)
                {
                    ctx.Session["AktifSepet"] = new Sepet();
                    return (Sepet)ctx.Session["AktifSepet"];
                }
                else
                {
                    return (Sepet)ctx.Session["AktifSepet"];
                }
            }
        }
    
        private List<SepetItem> urunler = new List<SepetItem>();
        public List<SepetItem> Urunler {
            get { return urunler; }
            set { urunler = value; }
        }
        public void UrunuSil(SepetItem sepet)
        {
            if (HttpContext.Current.Session["AktifSepet"] != null)
            {
                Sepet spet = (Sepet)HttpContext.Current.Session["AktifSepet"];

                if (spet.Urunler.Any(x=>x.urunadi==sepet.urunadi && x.fiyati==sepet.fiyati && x.adeti==sepet.adeti))
                {
                    var sepetteki = spet.Urunler.FirstOrDefault(x => x.urunadi == sepet.urunadi && x.fiyati == sepet.fiyati && x.adeti == sepet.adeti);
                    spet.Urunler.Remove(sepetteki);
                    HttpContext.Current.Session["AktifSepet"] = spet;
                }
            }
        }
        public void SepetAdetDegistir(SepetItem sepet)
        {
            if (HttpContext.Current.Session["AktifSepet"] != null)
            {
                Sepet spet = (Sepet)HttpContext.Current.Session["AktifSepet"];

                if (spet.Urunler.Any(x => x.urunadi == sepet.urunadi && x.fiyati == sepet.fiyati && x.urunid==sepet.urunid && x.firmaid==sepet.firmaid))
                {
                    var sepetteki = spet.Urunler.FirstOrDefault(x => x.urunadi == sepet.urunadi && x.fiyati == sepet.fiyati && x.firmaid==sepet.firmaid && x.urunid==sepet.urunid);
                    spet.Urunler.Remove(sepetteki);
                    HttpContext.Current.Session["AktifSepet"] = spet;
                    SepetItem urun = new SepetItem();
                    urun.urunid = sepet.urunid;
                    urun.urunadi = sepet.urunadi;
                    urun.fiyati = sepet.fiyati;
                    urun.firmaid = sepet.firmaid;
                    urun.adeti = sepet.adeti;
                    spet.Urunler.Add(urun);
                }
            }
        }
        public void SepeteEkle(SepetItem sepet)
        {
            if (HttpContext.Current.Session["AktifSepet"]!=null)
            {
                Sepet spet = (Sepet)HttpContext.Current.Session["AktifSepet"];
                if (spet.Urunler.Any(x => x.urunid == sepet.urunid))
                {
                    spet.Urunler.FirstOrDefault(x => x.urunid == sepet.urunid).adeti += sepet.adeti;
                }
                else
                {
                    spet.Urunler.Add(sepet);
                }

            }
            else
            {
                Sepet spet = new Sepet();
                spet.Urunler.Add(sepet);
                HttpContext.Current.Session["AktifSepet"] = spet;


            }
            
          }
          public decimal ToplamTutar
          {
            get
            {
                return Urunler.Sum(x => x.Tutar);
            }
          }
    }
        
        public  class SepetItem
        {
            public int urunid { get; set; }
            public string urunadi{ get; set; }
            public decimal fiyati { get; set; }
            public int adeti { get; set; }
            public int firmaid { get; set; }
            public decimal Tutar
            {
                get
                {
                       return (decimal)fiyati * adeti;
                }
            }
      }
}