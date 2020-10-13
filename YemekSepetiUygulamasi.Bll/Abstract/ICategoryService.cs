using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        List<Category> GetProductsByOgrenciId(int OgrenciId);
        List<Category> GetProductsByOgrenciName(string ogrenciName);
        Category Get(int Id);
        Category Getircategorisim(string categoriname);
        Category Update(Category entity);
        void Delete(Category entity);
        Category Add(Category entity);
    }
}
