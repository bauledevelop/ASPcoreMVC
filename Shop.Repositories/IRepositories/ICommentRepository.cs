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
        long GetTotal();
        IEnumerable<Comment> SelectByQuantityItem(int page, int pageSize);
        IQueryable<Comment> SelectAll();
        Comment SelectById(long id);
        void Insert(Comment obj);
        Task Update(Comment obj);
        void Delete(object id);
        void DeleteByItem(Comment id);
        void Save();
    }
}
