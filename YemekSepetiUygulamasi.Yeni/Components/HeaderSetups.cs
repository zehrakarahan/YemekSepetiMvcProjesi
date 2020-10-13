using MarketE_Commerce_Site.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Components
{
    public class HeaderSetups : ViewComponent
    {
        public IMainPageSetupsRepository repository;
        public HeaderSetups(IMainPageSetupsRepository _repository)
        {
            repository = _repository;
        }
        public IViewComponentResult Invoke()
        {
            return View(repository.GetAll().FirstOrDefault());
        }
    }
}
