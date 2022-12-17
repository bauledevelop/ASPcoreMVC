using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.IRepositories
{
    public interface IOrderRepository
    {
        IQueryable<Order> SelectByIDAccount(long IDAccount);
        long InsertOrder(Order order);
        IQueryable<Order> SelectByQuantityItem(int page, int pageSize);
        long GetTotal();
        IEnumerable<Order> SelectAll();
        Order SelectById(long id);
        void Insert(Order obj);
        Task Update(Order obj);
        void Delete(object id);
        void DeleteByItem(Order id);
        void Save();
    }
}
