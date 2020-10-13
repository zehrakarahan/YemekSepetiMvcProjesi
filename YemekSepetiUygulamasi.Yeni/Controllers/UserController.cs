using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MarketE_Commerce_Site.IdentityCore;
using MarketE_Commerce_Site.Models;
using MarketE_Commerce_Site.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketE_Commerce_Site.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUsers> userManager;
        private readonly IUnitOfWorkRepository uow;
        public UserController(UserManager<ApplicationUsers> _userManager, IUnitOfWorkRepository _uow)
        {
            userManager = _userManager;
            uow = _uow;
        }
        public async Task<IActionResult> Index()
        {
            //CheckoutLines dan Quantity silincek
            //CheckOutsdan da Total silincek

            if (!User.Identity.IsAuthenticated)
                RedirectToAction("Account", "AccessDeniedPage");

            var user = await userManager.GetUserAsync(User);
            var UserCart = uow.CheckOutLine.Find(x => x.UserId == user.Id).ToList();

            var userModelList = new List<UserAndCartInformationModel>();
            UserAndCartInformationModel usermodel = new UserAndCartInformationModel();

            if (UserCart.Count() != 0)
            {
                foreach (var item in UserCart)
                {
                
                    usermodel.Name = user.Name;
                    usermodel.Email = user.Email;
                    usermodel.CardId = item.CheckOutNumber;
                    usermodel.ProductsName = item.ProductCodes;
                    usermodel.CheckOutDate = item.CheckOutDate;
                    usermodel.Total = item.Total;
                    userModelList.Add(usermodel);
                }
            }
            else
            {
                usermodel.Name = user.Name;
                usermodel.Email = user.Email;
                userModelList.Add(usermodel);
            }
            return View(userModelList);
        }
    }
}