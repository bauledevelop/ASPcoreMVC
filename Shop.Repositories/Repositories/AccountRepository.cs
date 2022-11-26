using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Repositories.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly ShopContext _dbContext;
        public AccountRepository(ShopContext shopContext) : base(shopContext)
        {
            _dbContext = shopContext;
        }
        public Account GetAccountByUsername(string username)
        {
            return _dbContext.Accounts.SingleOrDefault(a => a.Username == username && a.Status == true);
        }
        public bool CheckUsername(string username)
        {
            var model = _dbContext.Accounts.FirstOrDefault(a => a.Username == username && a.Status == true);
            return (model == null ? false : true);
        }
        public bool CheckPassword(string password)
        {
            var model = _dbContext.Accounts.FirstOrDefault(a => a.Password == password && a.Status == true);
            return (model == null ? false : true);
        }
        public Account SelectAccountById(long id)
        {
            return _dbContext.Accounts.SingleOrDefault(x => x.ID == id);
        }
        public string GetPhoneByUsername(string username)
        {
            return _dbContext.Accounts.SingleOrDefault(x => x.Username == username).Phone;
        }
        public Account GetAccountById(long id)
        {
            return _dbContext.Accounts.SingleOrDefault(x => x.ID == id);
        }
        public string GetEmailByUsername(string username)
        {
            return _dbContext.Accounts.SingleOrDefault(x => x.Username == username).Email;
        }
        public bool CheckPhone(string phone)
        {
            var model = _dbContext.Accounts.SingleOrDefault(x => x.Phone == phone);
            return (model == null ? true : false);
        }
        public bool CheckEmail(string email)
        {
            var model = _dbContext.Accounts.SingleOrDefault(x => x.Email == email);
            return (model == null ? true : false);
        }
        public long GetTotal()
        {
            return _dbContext.Accounts.Where(x => x.IsDelete == false).Count();
        }
        public IQueryable<Account> SelectByQuantityItem(int page,int pageSize)
        {
            var model = _dbContext.Accounts.Where(x => x.IsDelete == false).Skip((page - 1) * pageSize).Take(pageSize);
            return model;
        }
    }
}
