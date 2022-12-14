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
        IQueryable<Product> SelectByKeyWord(string search);
        IQueryable<Product> SelectByKeyWordQuantityItem(int page, int pageSize, string search);
        IQueryable<Product> SelectAllProduct();
        Product GetByID(long id);
        IQueryable<Product> SelectRelatedProduct(long IDCategory, long ID);
        IQueryable<Product> SelectNewProduct();
        long GetTotalByIDCategory(long IDCategory);
        IQueryable<Product> SelectByIDCategoryQuantityItem(int page, int pageSize, long IDCategory);
        IQueryable<Product> SelectByIDCategory(long IDCategory);
        IQueryable<Product> SelectByQuantityItem(int page, int pageSize);
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
