using MarketE_Commerce_Site.Entity;
using MarketE_Commerce_Site.Models;
using MarketE_Commerce_Site.Repository.Abstract;
using MarketE_Commerce_Site.Repository.Concrete.EntityFrameWork;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Components
{
    public class MenuCategorySubCategory : ViewComponent
    {
        private readonly IConfiguration configuration;

        public MenuCategorySubCategory(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public IViewComponentResult Invoke()
        {
            var allMenuList = new List<MenuItemModel>();


            //var menus = from categories in dbContext.Categories
            //            join subcategories in dbContext.SubCategories on
            //            categories.CategoriesId equals subcategories.CategoriesId into gj
            //            from test in gj.DefaultIfEmpty()
            //            select new
            //            {
            //                categories.CategoriesName,
            //                test.SubCategoriesName
            //            };

            //idiots Things :@ !!
            //This Bloc USe For MsSql
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            //var commandText = "select CategoriesId,CategoriesName, STUFF((SELECT ', ' + c.SubCategoriesName FROM SubCategories c where c.CategoriesId = t.CategoriesId ORDER BY c.CategoriesId FOR XML PATH('')), 1, 1, '') AS Name From Categories t;";
            //using (var da = new SqlDataAdapter(commandText, connectionString))
            //{
            //    da.Fill(ds);
            //}

            var connectionString = configuration.GetConnectionString("EcoomerceMySqlConneciton");
            var commandTextForMysql = " SELECT ta.CategoriesId,ta.CategoriesName, GROUP_CONCAT(u.SubCategoriesName, ' ') AS fullname FROM Categories ta INNER JOIN Subcategories u ON u.CategoriesId = ta.CategoriesId GROUP BY   ta.CategoriesId,ta.CategoriesName;";
            var ds = new DataSet();
          
            using (var da = new MySqlDataAdapter(commandTextForMysql, connectionString))
            {
                da.Fill(ds);
            }

            foreach (DataRow menuItem in ds.Tables[0].Rows)
            {
                var menu = new MenuItemModel();
                menu.CategoryId = Convert.ToInt32(menuItem.ItemArray[0]);
                menu.CategoryName = menuItem.ItemArray[1].ToString();
                menu.SubCategoryName = menuItem.ItemArray[2].ToString();
                allMenuList.Add(menu);
            }

            return View(allMenuList);
        }
    }
}
//select
//       CategoriesName,
//       STUFF((SELECT ', ' + c.SubCategoriesName
//             FROM SubCategories c
//             where c.CategoriesId = t.CategoriesId
//             ORDER BY c.CategoriesId
//             FOR XML PATH('')), 1, 1, '') AS Name
//From Categories  t
