using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.IRepositories
{
    public interface ISlideRepository
    {
        IQueryable<Slide> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<Slide> SelectAll();
        Slide SelectById(long id);
        void Insert(Slide obj);
        Task Update(Slide obj);
        void Delete(object id);
        void DeleteByItem(Slide id);
        void Save();
    }
}
