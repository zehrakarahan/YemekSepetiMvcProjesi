using MarketE_Commerce_Site.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketE_Commerce_Site.Repository.Concrete.EntityFrameWork
{
    public static class SeedData
    {
        public static void ensurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetRequiredService<EcommerceDbContext>();
            context.Database.Migrate();

            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Categories() { CategoriesName="Temel Gıda"},
                    new Categories() { CategoriesName="Şarküteri"},
                    new Categories() { CategoriesName="Atıştırmalık"}

                };
                context.Categories.AddRange(categories);
          
                var subCategories = new[]
                {
                    new SubCategories() {SubCategoriesName="Tuz,Baharat,Bulyon",CategoriesId=1},
                    new SubCategories() {SubCategoriesName="Süt Ve Süt Ürünleri",CategoriesId=2},
                    new SubCategories() {SubCategoriesName="Kuruyemiş Ve Cips",CategoriesId=3},
                    new SubCategories() {SubCategoriesName="Tuz",CategoriesId = 1},
                    new SubCategories() {SubCategoriesName="Bulyon",CategoriesId = 1}

                };
                context.SubCategories.AddRange(subCategories);
          
                var products = new[]
                {
                    new Products(){ProductName="Billur İyotlu Tuz 750 Gr",CategoryId=1,SubCategoryId=4,ProductDescription="Bu ürün, şu anda mağaza stoğunda bulunmamaktadır.",NetProductPrice=7.99, ProductImage="Tuz.jpg",Bestsellers=true},
                    new Products(){ProductName="Saray İyotlu Tuz 1500 Gr",CategoryId=1,SubCategoryId=4,ProductDescription="Bu ürün, şu anda mağaza stoğunda bulunmamaktadır.",NetProductPrice=3.99, ProductImage="Tuz.jpg",Bestsellers=true},
                    new Products(){ProductName="Sütaş Süt Yarım Yağlı Uht 1 Lt",CategoryId=2,SubCategoryId=2,ProductDescription="Bu ürün, şu anda mağaza stoğunda bulunmamaktadır.",NetProductPrice=2.26, ProductImage="Sut.jpg",Bestsellers=true},
                    new Products(){ProductName="Danone Pro+ Muz & Yer Fıstıklı Proteinli Süt 330 M",CategoryId=2,SubCategoryId=2,ProductDescription="Bu ürün, şu anda mağaza stoğunda bulunmamaktadır.",NetProductPrice=4.26, ProductImage="ProteinliSut.jpg",Bestsellers=true},
                    new Products(){ProductName="Bizim Mutfak Tavuk Bulyon 120 Gr",CategoryId=1,SubCategoryId=5,ProductDescription="Bu ürün, şu anda mağaza stoğunda bulunmamaktadır.",NetProductPrice=3.26, ProductImage="Bulyon.jpg",Bestsellers=true},


                };
                context.Products.AddRange(products);
            }
            if (!context.CheckOutLines.Any())
            {
                var checkoutLine = new[]
                {
                    new CheckOutLine() {ProductCodes="test",Quantity=1,Total=22,UserId="176b740e-61d7-4cc9-b6ad-a0cae84361d3"}
                };
                context.CheckOutLines.AddRange(checkoutLine);
            }
            if (!context.CheckOuts.Any())
            {
                var checkout = new[]
                {
                    new CheckOut() {FirstName="ilker",Total=200 }
                };
                context.CheckOuts.AddRange(checkout);
            }
            context.SaveChanges();
        }
    }
}

