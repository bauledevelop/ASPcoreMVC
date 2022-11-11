using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.IRepositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> SelectAll();
        Product SelectById(object id);
        void Insert(Product obj);
        Task Update(Product obj);
        void Delete(object id);
        void DeleteByItem(Product id);
        void Save();
    }
}
