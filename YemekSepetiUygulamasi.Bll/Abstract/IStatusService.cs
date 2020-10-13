using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface IStatusService
    {
        List<Status> GetAll();
        List<Status> GetProductsByOgrenciId(int OgrenciId);
        List<Status> GetProductsByOgrenciName(string ogrenciName);
        Status Get(int Id);
        Status Update(Status entity);
        void Delete(Status entity);
        Status Add(Status entity);
    }
}
