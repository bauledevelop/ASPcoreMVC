using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.IRepositories
{
    public interface IMenuRepository
    {
        IEnumerable<Menu> SelectAllByDelete();
        long GetTotal();
        IEnumerable<Menu> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<Menu> SelectAll();
        Menu SelectById(long id);
        void Insert(Menu obj);
        Task Update(Menu obj);
        void Delete(object id);
        void DeleteByItem(Menu id);
        void Save();
    }
}
