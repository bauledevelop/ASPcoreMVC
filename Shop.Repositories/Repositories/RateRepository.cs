using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Repositories.Repositories
{
    public class RateRepository : GenericRepository<Rate>, IRateRepository
    {
        private readonly ShopContext _dbContext;
        public RateRepository(ShopContext shopContext) : base(shopContext)
        {
            _dbContext = shopContext;
        }
        public IQueryable<Rate> SelectByIDProduct(long idProduct)
        {
            try
            {
                var model = _dbContext.Rates.Where(x => x.IDProduct == idProduct);
                return model;
            }
            catch
            {
                return null;
            }
        }
        public IQueryable<Rate> GetRateByIDAccount(long idAccount)
        {
            try
            {
                var model = _dbContext.Rates.Where(x => x.IDAccount == idAccount);
                return model;
            }
            catch
            {
                return null;
            }
        }
    }
}
