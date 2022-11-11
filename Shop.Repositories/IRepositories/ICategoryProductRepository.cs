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
        IEnumerable<CategoryProduct> SelectAll();
        CategoryProduct SelectById(object id);
        void Insert(CategoryProduct obj);
        Task Update(CategoryProduct obj);
        void Delete(object id);
        void DeleteByItem(CategoryProduct id);
        void Save();
    }
}
