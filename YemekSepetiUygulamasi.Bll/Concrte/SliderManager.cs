using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;
using YemekSepetiUygulamasi.Bll.Abstract;
using YemekSepetiUygulamasi.Dal.Abstract;

namespace YemekSepetiUygulamasi.Bll.Concrte
{
    public class SliderManager : ISliderService
    {
        private ISliderDal _SliderDal;
        public SliderManager(ISliderDal SliderDal)
        {
            _SliderDal = SliderDal;
        }
        public Slider Add(Slider entity)
        {
            return _SliderDal.Add(entity);
        }

        public void Delete(Slider entity)
        {
             _SliderDal.Delete(entity);
        }

        public Slider Get(int Id)
        {
            return _SliderDal.Get(x => x.Id == Id);
        }

        public List<Slider> GetAll()
        {
            return _SliderDal.GetAll();
        }

        public List<Slider> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Slider> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Slider Update(Slider entity)
        {
            return _SliderDal.Update(entity);
        }
    }
}
