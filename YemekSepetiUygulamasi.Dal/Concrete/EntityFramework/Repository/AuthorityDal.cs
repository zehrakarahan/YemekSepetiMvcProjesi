using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess.EntityFramework;
using YemekSepetiUygulamasi.Dal.Abstract;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Dal.Concrete.EntityFramework.Repository
{
    public class AuthorityDal : EfEntityRepositoryBase<Authority, YemekSeyetContextt>, IAuthorityDal
    {
    }
}
