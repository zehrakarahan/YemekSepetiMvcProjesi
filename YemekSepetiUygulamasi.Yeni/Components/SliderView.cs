using MarketE_Commerce_Site.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Components
{
    public class SliderView : ViewComponent
    {
        private IUnitOfWorkRepository uow;
        public SliderView(IUnitOfWorkRepository _uow)
        {
            uow = _uow;
        }
        public IViewComponentResult Invoke(int SlideNumber)
        {
            var getAllslideR = uow.MainSliderSetup.GetAll();
        
            if (SlideNumber == 0)
            {
                ViewBag.SecondSliderIsActive = "false";
                return View(getAllslideR.Where(b => b.IsActiveFirsSlider == true && b.IsDisable == false).ToList());
            }
            else
            {
                ViewBag.SecondSliderIsActive = "true";
                return View(getAllslideR.Where(b => b.IsActiveSecondSlider == true && b.IsDisable == false).ToList());
            }
        }

    }
}
