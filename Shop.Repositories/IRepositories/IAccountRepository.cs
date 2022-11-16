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
        Account SelectAccountById(long id);
        bool CheckPhone(string phone);
        string GetPhoneByUsername(string username);
        Account GetAccountById(long id);
        string GetEmailByUsername(string username);
        bool CheckEmail(string email);
        long GetTotal();
        IEnumerable<Account> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<Account> SelectAll();
        Account SelectById(long id);
        void Insert(Account obj);
        Task Update(Account obj);
        void Delete(object id);
        void DeleteByItem(Account id);
        void Save();
    }
}
