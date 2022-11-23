using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.IRepositories
{
    public interface IRateRepository
    {
        IEnumerable<Rate> SelectAll();
        Rate SelectById(long id);
        void Insert(Rate obj);
        Task Update(Rate obj);
        void Delete(object id);
        void DeleteByItem(Rate id);
        void Save();
    }
}
