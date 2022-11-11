using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Repositories
{
    public class CategoryProductRepository : GenericRepository<CategoryProduct>, ICategoryProductRepository
    {
        private readonly ShopContext _dbContext;
        public CategoryProductRepository(ShopContext shopContext) : base(shopContext)
        {
            _dbContext = shopContext;
        }
    }
}
