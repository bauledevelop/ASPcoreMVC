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
        Product GetByID(long id);
        IEnumerable<Product> SelectRelatedProduct(long IDCategory, long ID);
        IEnumerable<Product> SelectNewProduct();
        long GetTotalByIDCategory(long IDCategory);
        IEnumerable<Product> SelectByIDCategoryQuantityItem(int page, int pageSize, long IDCategory);
        IEnumerable<Product> SelectByIDCategory(long IDCategory);
        IEnumerable<Product> SelectByQuantityItem(int page, int pageSize);
        long GetTotal();
        IEnumerable<Product> SelectAll();
        Product SelectById(long id);
        void Insert(Product obj);
        Task Update(Product obj);
        void Delete(object id);
        void DeleteByItem(Product id);
        void Save();
    }
}
