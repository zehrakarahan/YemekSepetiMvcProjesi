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
    public class EmployeesManager : IEmployeesService
    {
        private IEmployeesDal _EmployeesDal;
        public EmployeesManager(IEmployeesDal EmployeesDal)
        {
            _EmployeesDal = EmployeesDal;
        }
        public Employess Add(Employess entity)
        {
            return _EmployeesDal.Add(entity);
        }

        public void Delete(Employess entity)
        {
            _EmployeesDal.Delete(entity);
        }

        public Employess Get(int Id)
        {
            return _EmployeesDal.Get(x => x.Id == Id);
        }

        public List<Employess> GetAll()
        {
            return _EmployeesDal.GetAll();   
        }

        public List<Employess> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Employess> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Employess Update(Employess entity)
        {
            return _EmployeesDal.Update(entity);
        }
    }
}
