using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ShopContext _dbContext;
        public OrderDetailRepository(ShopContext shopContext) : base(shopContext)
        {
            _dbContext = shopContext;
        }
        public IQueryable<OrderDetail> SelectByIDOrder(long idOrder)
        {
            try
            {
                var model = _dbContext.OrderDetails.Where(x => x.IDOrder == idOrder);
                return model;
            }
            catch
            { return null;
            }
        }
        public IQueryable<OrderDetail> SelectByIDProduct(long id)
        {
            try
            {
                var model = _dbContext.OrderDetails.Where(x => x.IDProduct == id);
                return model;
            }
            catch
            {
                return null;
            }
        }
        public IQueryable<OrderDetail> SelectAllOrderDetail()
        {
            try
            {
                var model = _dbContext.OrderDetails.Where(x => x.Status == true);
                return model;
            }
            catch
            {
                return null;
            }
        }
    }
}
