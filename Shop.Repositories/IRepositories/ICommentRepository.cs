using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.IRepositories
{
    public interface ICommentRepository
    {
        IQueryable<Comment> SelectByIDProduct(long idProduct,int page,int pageSize);
        long GetTotalByIDProduct(long productID);
        Comment GetCommentByIDAccount(long IDAccount);
        long GetTotal();
        IQueryable<Comment> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<Comment> SelectAll();
        Comment SelectById(long id);
        void Insert(Comment obj);
        Task Update(Comment obj);
        void Delete(object id);
        void DeleteByItem(Comment id);
        void Save();
    }
}
