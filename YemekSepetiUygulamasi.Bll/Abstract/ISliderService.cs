using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ISliderService
    {
        List<Slider> GetAll();
        List<Slider> GetProductsByOgrenciId(int OgrenciId);
        List<Slider> GetProductsByOgrenciName(string ogrenciName);
        Slider Get(int Id);
        Slider Update(Slider entity);
        void Delete(Slider entity);
        Slider Add(Slider entity);
    }
}
