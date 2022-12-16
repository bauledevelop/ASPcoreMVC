using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.IRepositories
{
    public interface ICategoryProductRepository
    {
        IQueryable<CategoryProduct> SelectAllByStatus();
        IQueryable<CategoryProduct> SelectByIDMenu(long idMenu);
        IQueryable<CategoryProduct> SelectAllByDelete();
        IQueryable<CategoryProduct> SelectByQuantityItem(int page, int pageSize);
        long GetTotal();
        IEnumerable<CategoryProduct> SelectAll();
        CategoryProduct SelectById(long id);
        void Insert(CategoryProduct obj);
        Task Update(CategoryProduct obj);
        void Delete(object id);
        void DeleteByItem(CategoryProduct id);
        void Save();
    }
}
