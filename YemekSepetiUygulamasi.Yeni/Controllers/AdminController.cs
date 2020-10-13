using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MarketE_Commerce_Site.Entity;
using MarketE_Commerce_Site.IdentityCore;
using MarketE_Commerce_Site.Models;
using MarketE_Commerce_Site.Repository.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketE_Commerce_Site.Controllers
{
   [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        public IUnitOfWorkRepository uow;
        private UserManager<ApplicationUsers> users;
        private RoleManager<IdentityRole> roles;

        public AdminController(IUnitOfWorkRepository _uow, UserManager<ApplicationUsers> _users, RoleManager<IdentityRole> _roles)
        {
            uow = _uow;
            users = _users;
            roles = _roles;
        }

        public IActionResult Index()
        {
            List<AdminProdcutListModel> allProduct = new List<AdminProdcutListModel>();

            allProduct = uow.Product.GetAll().Select(b => new AdminProdcutListModel()
            {
                ProductId = b.ProductsId,
                ProductName = b.ProductName,
                ProductDescription = b.ProductDescription,
                ProductSmallDescription = b.ProductSmallDescription,
                DateAdded = b.DateAdded,
                Quantity = b.ProductQuantity,
                CategoryName = uow.Category.GetAll()
                .Where(c => c.CategoriesId == b.CategoryId)
                .Select(c => c.CategoriesName).FirstOrDefault(),

                SubCategoryName = uow.Subcategory.GetAll()
                .Where(d => d.SubCategoriesId == b.SubCategoryId)
                .Select(d => d.SubCategoriesName).FirstOrDefault(),


            }).ToList();

            ViewBag.ProductCount = allProduct.Count();


            return View(allProduct);
        }

        public IActionResult GetCategoryList()
        {
            var category = uow.Category.GetAll().ToList();
            ViewBag.CategoryList = category;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(Categories model)
        {
            if (ModelState.IsValid)
            {
                uow.Category.Add(model);
                uow.SaveChanges();
                TempData["message"] = $"{model.CategoriesName} is Save to Categories";
                return RedirectToAction("GetCategoryList", "Admin");
            }
            return View(model);
        }


        public IActionResult RemoveCategory(int Category_id)
        {
            var GetRemoveCategory = uow.Category.GetById(Category_id);

            uow.Category.Delete(GetRemoveCategory);
            TempData["message"] = $"{GetRemoveCategory.CategoriesName} is Removed to Categories";
            uow.SaveChanges();

            return RedirectToAction("GetCategoryList", "Admin");
        }


        [HttpPost]
        public IActionResult GetCategory(int Category_id)
        {
            var EditCategory = uow.Category.GetById(Category_id);

            return PartialView("UpdateCategoryModal", EditCategory);
        }
        [HttpPost]
        public IActionResult EditCategory(int Category_id, string Category_name)
        {
            var categories = uow.Category.GetById(Category_id);
            if (categories.CategoriesName == Category_name)
            {
                return View();
            }
            else
            {
                categories.CategoriesName = Category_name;
                uow.Category.Edit(categories);
                TempData["message"] = $"Category Name is Changed To {Category_name}";
                uow.SaveChanges();
            }
            return Ok();
        }


        public IActionResult GetSubCategoryList()
        {
            var subCategory = uow.Subcategory.GetAll().Select(b => new SubCategoryCategoryModel()
            {
                SubCategoryId = b.SubCategoriesId,
                SubCategoryName = b.SubCategoriesName,
                CategoryId = b.CategoriesId,
                CategoryName = uow.Category.GetAll()
                .Where(d => d.CategoriesId == b.CategoriesId)
                .Select(b => b.CategoriesName).FirstOrDefault()
            }).ToList();
            ViewBag.SubCategoryList = subCategory;
            ViewBag.CategoryList = new SelectList(uow.Category.GetAll().ToList(), "CategoriesId", "CategoriesName");
            return View();
        }
        public IActionResult RemoveSubCategories(int subCategory_id)
        {
            var subCategory = uow.Subcategory.GetById(subCategory_id);
            uow.Subcategory.Delete(subCategory);
            TempData["messages"] = $"{subCategory.SubCategoriesName} Sub Category is Removed";
            uow.SaveChanges();
            return RedirectToAction("GetSubCategoryList", "Admin");
        }

        //CAtegory Nameleri Gelmiyor Hatali duzelt Onu
        [HttpPost]
        public IActionResult AddNewSubCategories(SubCategoryCategoryModel model)
        {
            var NewsubcategoryModel = new SubCategories();
            if (model != null)
            {
                NewsubcategoryModel.CategoriesId = model.CategoryId;
                NewsubcategoryModel.SubCategoriesName = model.SubCategoryName;
                uow.Subcategory.Add(NewsubcategoryModel);
                uow.SaveChanges();
            }
            return RedirectToAction("GetSubCategoryList", "Admin");
        }

        [HttpPost]
        public IActionResult EditSubCategory(int subCategory_id)
        {
            var sub_Category_Edit = uow.Subcategory.GetById(subCategory_id);
            ViewBag.CategoryList = new SelectList(uow.Category.GetAll().ToList(), "CategoriesId", "CategoriesName");
            return PartialView("UpdateSubCategoryModal", sub_Category_Edit);
        }
        [HttpPost]
        public IActionResult ChangeAndSaveEdittedSubCategory(int subCategory_id, string newSubCategoryName, int newCategory_id)
        {
            var SubCategoryContent = uow.Subcategory.GetById(subCategory_id);
            if (SubCategoryContent.SubCategoriesName == newSubCategoryName && SubCategoryContent.CategoriesId == newCategory_id)
            {
                return RedirectToAction("GetSubCategoryList", "Admin");
            }
            else
            {
                SubCategoryContent.CategoriesId = newCategory_id;
                SubCategoryContent.SubCategoriesName = newSubCategoryName;
                SubCategoryContent.CategoriesId = newCategory_id;
                uow.Subcategory.Edit(SubCategoryContent);
                TempData["message"] = $"SubCategory Name is Changed To {newSubCategoryName}";
                uow.SaveChanges();
            }
            return Ok();
        }

        public IActionResult AddNewProduct(int product_id)
        {
            ViewBag.AllCategories = new SelectList(uow.Category.GetAll().ToList(), "CategoriesId", "CategoriesName");
            ViewBag.AllSubCategories = new SelectList(uow.Subcategory.GetAll().ToList(), "SubCategoriesId", "SubCategoriesName");
            if (product_id == 0)
            {
                return View(new Products() { DateAdded = DateTime.Now });
            }
            else
            {
                return View(uow.Product.GetById(product_id));
            }


        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(Products model, IFormFile file)
        {
            string savedPath = "wwwroot\\Productimage";
            var Product = uow.Product.GetById(model.ProductsId);
            if (Product == null)
            {
                //DateTime Hatalı duzelt
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {

                        Task<bool> res = GetImageFromFile(file, savedPath);
                        if (await res)
                        {
                            model.ProductImage = file.FileName;
                        }
                    }
                    uow.Product.Add(model);
                    uow.SaveChanges();
                    TempData["message"] = $"{model.ProductName} is succesfuly saved.";
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                Product.ProductName = model.ProductName;
                Product.ProductDescription = model.ProductDescription;
                Product.ProductSmallDescription = model.ProductSmallDescription;
                Product.NetProductPrice = model.NetProductPrice;
                Product.BrutProductPrice= model.BrutProductPrice;
                Product.ShippingPrice= model.ShippingPrice;
                Product.ProductQuantity = model.ProductQuantity;
                if (file != null && Product.ProductImage !=file.FileName)
                {
                    //Tek bir method yazz
                    if (file != null)
                    {
                        Task<bool> res = GetImageFromFile(file, savedPath);
                        if (await res)
                        {
                            Product.ProductImage = file.FileName;
                        }
                    }
                }
                Product.IsNew = model.IsNew;
                Product.IsFeatured = model.IsFeatured; // silcez
                Product.Bestsellers = model.Bestsellers; // bunuda IsBestSeller yap
                Product.DateAdded = model.DateAdded;
                Product.IsApproved = model.IsApproved; // gereksizse sil zaten is active ile aynı amac
                Product.CategoryId = model.CategoryId;
                Product.SubCategoryId = model.SubCategoryId;
                Product.productIsActive = model.productIsActive;
                Product.ShippingDescription = model.ShippingDescription;
                Product.cancellationProcedure = model.cancellationProcedure;

                uow.Product.Edit(Product);
                TempData["message"] = $"{model.ProductName} Is Updated Succesfully.";
                uow.SaveChanges();

                return RedirectToAction("Index", "Admin");
            }


        }
        private async Task<bool> GetImageFromFile(IFormFile file, string pSavedPath)
        {
            string err = "";
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), pSavedPath, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }

        }

        public IActionResult RemoveProduct(int Product_id)
        {
            var getRemoveProduct = uow.Product.GetById(Product_id);

            string currentPath = "wwwroot\\Productimage\\" +getRemoveProduct.ProductImage;
            var _imagePath = Path.Combine(Directory.GetCurrentDirectory(), currentPath);
            if (System.IO.File.Exists(_imagePath))
            {
                System.IO.File.Delete(_imagePath);
            }

            uow.Product.Delete(getRemoveProduct);
            TempData["message"] = $"{Product_id} Numbered Product Removed Succesfully";
            uow.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult MainSliderSetups(int sliderId)
        {
            ViewBag.SliderList = uow.MainSliderSetup.GetAll().ToList();
            if (sliderId == 0)
            {
                return View(new MainSliderSetup());
            }
            else
            {
                var res = uow.MainSliderSetup.GetById(sliderId);
                return View(res);
            }

        }
        [HttpPost]
        public async Task<IActionResult> MainSliderSetups(MainSliderSetup model, IFormFile file)
        {
            string savedPath = "wwwroot\\SliderImage";
            if (model.MainSliderSetupId == 0)
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        //var path = Path.Combine(Directory.GetCurrentDirectory(), savedPath, file.FileName);
                        //using (var stream = new FileStream(path, FileMode.Create))
                        //{
                        //    await file.CopyToAsync(stream);
                        //    model.MainSliderImage = file.FileName;
                        //}
                        Task<bool> res = GetImageFromFile(file, savedPath);
                        if (await res)
                        {
                            model.MainSliderImage = file.FileName;
                        }
                    }
                    uow.MainSliderSetup.Add(model);
                    TempData["message"] = $"{file.FileName} Save to Succesfully";
                    uow.SaveChanges();

                }
            }
            else
            {
                var SlideR = uow.MainSliderSetup.GetById(model.MainSliderSetupId);
                SlideR.MainSliderSmallContex = model.MainSliderSmallContex;
                SlideR.MainSliderLongContex = model.MainSliderLongContex;
                SlideR.IsDisable = model.IsDisable;
                SlideR.IsActiveFirsSlider = model.IsActiveFirsSlider;
                SlideR.IsActiveSecondSlider = model.IsActiveSecondSlider;
                if (file != null && model.MainSliderImage != file.FileName)
                {
                    Task<bool> res = GetImageFromFile(file, savedPath);
                    if (await res)
                    {
                        SlideR.MainSliderImage = file.FileName;
                    }
                }
                uow.MainSliderSetup.Edit(SlideR);
                TempData["message"] = $"{model.MainSliderImage} Edit to Succesfully";
                uow.SaveChanges();
            }
            return RedirectToAction("MainSliderSetups", "Admin");
        }

        public IActionResult RemoveSlider(int SliderId)
        {
            var getSlider = uow.MainSliderSetup.GetById(SliderId);

            uow.MainSliderSetup.Delete(getSlider);
            TempData["message"] = $"{SliderId} is Removed Succesfully";
            uow.SaveChanges();

            return RedirectToAction("MainSliderSetups", "Admin");
        }

        public IActionResult EditMainPageSetups()
        {
            var IsAnyMainPageSetup = uow.MainPageSetup.GetAll();
            if (IsAnyMainPageSetup.Count() == 0)
            {
                return View(new MainPage());
            }
            else
            {
                var res = IsAnyMainPageSetup.FirstOrDefault();
                return View(res);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditMainPageSetups(MainPage model, IFormFile file)
        {
            string _path = "wwwroot//logos";
            var IsAnyMainPageSetup = uow.MainPageSetup.GetAll();
            if (IsAnyMainPageSetup.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        Task<bool> res = GetImageFromFile(file, _path);

                        if (await res)
                        {
                            model.LogoImage = file.FileName;
                        }
                    }
                    uow.MainPageSetup.Add(model);
                    TempData["message"] = $"Main Page Setups Saved To Successfully";
                    uow.SaveChanges();
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return View(model);
                }
            }
            else
            if (IsAnyMainPageSetup.Count() == 1)
            {
                {
                    var mainPageModel = uow.MainPageSetup.GetAll().FirstOrDefault();
                    if (ModelState.IsValid)
                    {
                        if (file != null && file.FileName != mainPageModel.LogoImage)
                        {
                            Task<bool> res = GetImageFromFile(file, _path);
                            if (await res)
                            {
                                mainPageModel.LogoImage = file.FileName;
                            }
                        }
                        mainPageModel.PhoneNumber1 = model.PhoneNumber1;
                        mainPageModel.PhoneNumber2 = model.PhoneNumber2;
                        mainPageModel.SkypeName = model.SkypeName;
                        mainPageModel.MailAdress = model.MailAdress;
                        mainPageModel.NewsHeader = model.NewsHeader;
                        mainPageModel.NewsContext = model.NewsContext;
                        mainPageModel.NewsIsActive = model.NewsIsActive;
                        //uow.MainPageSetup.Edit(mainPageModel); // bunu dene bi 
                        uow.MainPageSetup.Edit(mainPageModel);
                        TempData["message"] = $"Değişiklikler Kayıt Edildi";
                        uow.SaveChanges();
                    }
                }

            }
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> UserList()
        {
            var listUser = users.Users.ToList();
            var ListRoles = new List<UserAndRoleModels>();

            foreach (var user in listUser)
            {
                foreach (var role in await users.GetRolesAsync(user))
                {
                    var model = new UserAndRoleModels();
                    model.UserAndRoleModelsId = user.Id;
                    model.Name = user.Name;
                    model.Surname = user.Surname;
                    model.Email = user.Email;
                    model.RoleName = role;
                    ListRoles.Add(model);
                }

            }
            return View(ListRoles);
        }


        public async Task<IActionResult> EditUser(UserAndRoleModels model)
        {
            return RedirectToAction("UserList", "Admin");
        }

        public async Task<IActionResult> RemoveUser(int UserId)
        {

            if (UserId != 0)
            {
                string _userId = Convert.ToString(UserId);
                var getUser = await users.FindByIdAsync(_userId);
                string _roleId = Convert.ToString(getUser.Id);
                await users.DeleteAsync(getUser);
                TempData["message"] = $"{getUser.Name} Deleted Bu Succesfully";
                
                return RedirectToAction("UserList", "Admin");
            }
            else
            {
                return RedirectToAction("UserList", "Admin");
            }


        }

    }
}