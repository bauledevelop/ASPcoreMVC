using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Repositories
{
    public class SlideRepository : GenericRepository<Slide>, ISlideRepository
    {
        private readonly ShopContext _dbContext;
        public SlideRepository(ShopContext shopContext) : base(shopContext)
        {
            _dbContext = shopContext;
        }
        public IQueryable<Slide> SelectByQuantityItem(int page, int pageSize)
        {
            try
            {
                var model = _dbContext.Slides.Where(x => x.ID !=0).Skip((page - 1) * pageSize).Take(pageSize);
                return model;
            }
            catch
            {
                return null;
            }
        }
    }
}
