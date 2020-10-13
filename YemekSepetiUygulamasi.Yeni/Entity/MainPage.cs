using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Entity
{
    public class MainPage
    {
        public int MainPageId { get; set; }
        [Phone]
        public string PhoneNumber1 { get; set; }
        [Phone]
        public string PhoneNumber2 { get; set; }
        public string SkypeName { get; set; }
        public string MailAdress { get; set; }
        public string LogoImage { get; set; }
        public string NewsHeader { get; set; }
        public string NewsContext { get; set; }
        public bool NewsIsActive { get; set; }


    }
}
