using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YemekSepetiUygulamasi.WebUI.Models
{
    public class UyeBilgileri
    {
        public string isim { get; set; }

        public string soyisim { get; set; }
        public string Email { get; set; }
        public string parola { get; set; }
        public string ulke { get; set; }
        public string il { get; set; }
        public string ilce { get; set; }
        public string semt { get; set; }
        public string adres { get; set; }
    }
}