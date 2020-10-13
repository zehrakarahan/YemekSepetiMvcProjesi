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
    public class CommentManager : ICommentService
    {
        private ICommentDal _CommentDal;
        public CommentManager(ICommentDal CommentDal)
        {
            _CommentDal = CommentDal;
        }

        public Comment Add(Comment entity)
        {
            return _CommentDal.Add(entity);
        }

        public void Delete(Comment entity)
        {
            _CommentDal.Delete(entity); 
        }

        public Comment Get(int Id)
        {
            return _CommentDal.Get(x => x.Id == Id);
        }

        public List<Comment> GetAll()
        {
            return _CommentDal.GetAll();   
        }

        public List<Comment> Getir(int firmaId)
        {
            return _CommentDal.GetAll(x => x.CompaniesId == firmaId).ToList();
        }

        public List<Comment> GetProductsByOgrenciId(int OgrenciId)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetProductsByOgrenciName(string ogrenciName)
        {
            throw new NotImplementedException();
        }

        public Comment Update(Comment entity)
        {
            return _CommentDal.Update(entity);
        }
    }
}
