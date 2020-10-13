using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Entity
{
    public class MainSliderSetup
    {
        public int MainSliderSetupId { get; set; }
        public string MainSliderImage { get; set; }
        public string MainSliderSmallContex { get; set; }
        public string MainSliderLongContex { get; set; }
        public bool IsActiveFirsSlider { get; set; }
        public bool IsActiveSecondSlider { get; set; }
        public bool IsDisable { get; set; }




    }
}
