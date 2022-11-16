using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Repositories
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        private readonly ShopContext _dbContext;
        public MenuRepository(ShopContext shopContext) : base(shopContext)
        {
            _dbContext = shopContext;
        }
        public IEnumerable<Menu> SelectAllByDelete()
        {
            var model = _dbContext.Menus.Where(x => x.IsDelete == false);
            return model;
        }
        public IEnumerable<Menu> SelectByQuantityItem(int page, int pageSize)
        {
            var model = _dbContext.Menus.Where(x => x.IsDelete == false).Skip((page - 1) * pageSize).Take(pageSize);
            return model;
        }
        public long GetTotal()
        {
            var model = _dbContext.Menus.Where(x => x.IsDelete == false).Count();
            return model;
        }
    }
}
