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
        IEnumerable<Comment> SelectAll();
        Comment SelectById(object id);
        void Insert(Comment obj);
        Task Update(Comment obj);
        void Delete(object id);
        void DeleteByItem(Comment id);
        void Save();
    }
}
