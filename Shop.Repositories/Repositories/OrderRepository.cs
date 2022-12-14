using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ShopContext _dbContext;
        public OrderRepository(ShopContext shopContext) : base(shopContext)
        {
            _dbContext = shopContext;
        }
        public long InsertOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return order.ID;
        }
        public IQueryable<Order> SelectByQuantityItem(int page, int pageSize)
        {
            try
            {
                var model = _dbContext.Orders.Where(x => x.IsDelete == false).Skip((page - 1) * pageSize).Take(pageSize);
                return model;
            }
            catch
            {
                return null;
            }
        }
        public long GetTotal()
        {
            return _dbContext.Orders.Where(x => x.ID != 0).Count();
        }
    }
}
