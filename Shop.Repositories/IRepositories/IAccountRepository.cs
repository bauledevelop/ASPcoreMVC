using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.IRepositories
{
    public interface IAccountRepository
    {
        long GetTotal();
        IEnumerable<Account> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<Account> SelectAll();
        Account SelectById(object id);
        void Insert(Account obj);
        Task Update(Account obj);
        void Delete(object id);
        void DeleteByItem(Account id);
        void Save();
    }
}
