using AutoMapper;
using Business.Tool;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Implements
{
    public class AccountBusiness : IAccountBusiness
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountBusiness(IAccountRepository accountRepository,IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public IEnumerable<AccountDTO> SelectAll()
        {
            var accounts = _accountRepository.SelectAll();
            var accountDtos = accounts.Select(item => _mapper.Map<Account, AccountDTO>(item));
            return accountDtos;
        }
        public void DeleteAccount(long id)
        {
            var account = _accountRepository.SelectById(id);
            account.IsDelete = true;
            _accountRepository.Update(account);
            _accountRepository.Save();
        }
        public bool EditAccount(AccountDTO accountDTO)
        {
            var account = _mapper.Map<AccountDTO, Account>(accountDTO);
            var SHA1 = new HashPasswordSHA1();
            var email = _accountRepository.GetEmailByUsername(account.Username);
            var check = _accountRepository.CheckEmail(account.Email);
            if (check || email == accountDTO.Email)
            {
                account.Password = SHA1.HashPassword(account.Password);
                _accountRepository.Update(account);
                _accountRepository.Save();
                return true;
            }
            return false;
        }
        public AccountDTO GetAccountById(long id)
        {
            var account = _accountRepository.SelectById(id);
            var accountDto = _mapper.Map<Account, AccountDTO>(account);
            return accountDto;
        }
        public bool InsertAccount(AccountDTO accountDTO)
        {
            var account = _mapper.Map<AccountDTO, Account>(accountDTO);
            var SHA1 = new HashPasswordSHA1();
            account.CreatedDate = DateTime.Now;
            account.IsActive = true;
            account.IsDelete = false;
            var check = _accountRepository.CheckEmail(account.Email);
            if (check)
            {
                account.Password = SHA1.HashPassword(account.Password);
                _accountRepository.Update(account);
                _accountRepository.Save();
                return true;
            }
            return false;
        }
        public IEnumerable<AccountDTO> SelectByQuantityItem(int page,int pageSize)
        {
            var accounts = _accountRepository.SelectByQuantityItem(page, pageSize);
            var accountDtos = accounts.Select(item => _mapper.Map<Account, AccountDTO>(item));
            return accountDtos;
        }
        public long GetTotal()
        {
            return _accountRepository.GetTotal();
        }
    }
}
