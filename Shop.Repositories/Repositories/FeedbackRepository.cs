using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Repositories
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        private readonly ShopContext _dbContext;
        public FeedbackRepository(ShopContext shopContext) : base(shopContext)
        {
            _dbContext = shopContext;
        }
    }
}
