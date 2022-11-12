using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly ShopContext _dbContext;
        public AccountRepository(ShopContext shopContext) : base(shopContext)
        {
            _dbContext = shopContext;
        }
        public long GetTotal()
        {
            try
            {
                return _dbContext.Accounts.Where(x => x.IsDelete == false).Count();
            }
            catch
            {
                return 0;
            }
        }
        public IEnumerable<Account> SelectByQuantityItem(int page,int pageSize)
        {
            try
            {
                var model = _dbContext.Accounts.Where(x => x.IsDelete == false).Skip((page - 1) * pageSize).Take(pageSize);
                return model;
            }
            catch
            {
                return null;
            }
        }
    }
}
