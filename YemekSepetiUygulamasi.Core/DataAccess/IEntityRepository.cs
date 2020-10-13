using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YemekSepetiUygulamasi.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> filter);
        T Get2(Expression<Func<T, bool>> filter);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        List<T> Getsayi(Expression<Func<T, bool>> filter = null);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
