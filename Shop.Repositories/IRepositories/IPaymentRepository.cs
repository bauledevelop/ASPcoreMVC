using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.IRepositories
{
    public interface IPaymentRepository
    {
        IQueryable<Payment> SelectByQuantityItem(int page, int pageSize);
        long GetTotal();
        IEnumerable<Payment> SelectAll();
        Payment SelectById(long id);
        void Insert(Payment obj);
        Task Update(Payment obj);
        void Delete(object id);
        void DeleteByItem(Payment id);
        void Save();
    }
}
