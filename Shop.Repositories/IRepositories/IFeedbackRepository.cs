using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.IRepositories
{
    public interface IFeedbackRepository
    {
        IQueryable<Feedback> SelectByQuantityItem(int page, int pageSize);
        long GetTotal();
        IEnumerable<Feedback> SelectAll();
        Feedback SelectById(long id);
        void Insert(Feedback obj);
        Task Update(Feedback obj);
        void Delete(object id);
        void DeleteByItem(Feedback id);
        void Save();
    }
}
