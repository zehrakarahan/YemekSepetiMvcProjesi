using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemekSepetiUygulamasi.Core.DataAccess;
using YemekSepetiUygulamasi.Entity.EntityTable;

namespace YemekSepetiUygulamasi.Bll.Abstract
{
    public interface ICommentService
    {
        List<Comment> GetAll();
        List<Comment> GetProductsByOgrenciId(int OgrenciId);
        List<Comment> GetProductsByOgrenciName(string ogrenciName);
        List<Comment> Getir(int firmaId);
        Comment Get(int Id);
        Comment Update(Comment entity);
        void Delete(Comment entity);
        Comment Add(Comment entity);
    }
}
