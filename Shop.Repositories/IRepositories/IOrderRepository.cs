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
        IEnumerable<Order> SelectAll();
        Order SelectById(object id);
        void Insert(Order obj);
        Task Update(Order obj);
        void Delete(object id);
        void DeleteByItem(Order id);
        void Save();
    }
}
