using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;

namespace Shop.Repositories.Repositories
{
    public class RateRepository : GenericRepository<Rate>,IRateRepository
    {
        private readonly ShopContext _dbContext;
        public RateRepository(ShopContext shopContext) : base(shopContext)
        {
            _dbContext = shopContext;
        }
    }
}
