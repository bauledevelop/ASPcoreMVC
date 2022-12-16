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
        private readonly ICategoryProductBusiness _categoryProductBusiness;
        private readonly IProductBusiness _productBusiness;
        private readonly IFileBusiness _fileBusiness;
        private readonly ICommentBusiness _commentBusiness;
        private readonly IPaymentBusiness _paymentBusiness;
        private readonly IOrderBusiness _orderBusiness;
        public AccountBusiness(IAccountRepository accountRepository,IMapper mapper,
            ICategoryProductBusiness categoryProductBusiness,
            IProductBusiness productBusiness,
            IFileBusiness fileBusiness,
            ICommentBusiness commentBusiness,
            IPaymentBusiness paymentBusiness,
            IOrderBusiness orderBusiness
            )
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _categoryProductBusiness = categoryProductBusiness;
            _productBusiness = productBusiness;
            _fileBusiness = fileBusiness;
            _commentBusiness = commentBusiness;
            _paymentBusiness = paymentBusiness;
            _orderBusiness = orderBusiness;
        }
        public bool ChangeAccount(AccountDTO accountDTO)
        {
            var account = _mapper.Map<AccountDTO, Account>(accountDTO);
            var check = _accountRepository.CheckPhone(account.Phone);
            if (check == true)
            {
                _accountRepository.Update(account);
                _accountRepository.Save();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ResetPassword(string username, string password)
        {
            var account = _accountRepository.GetAccountByUsername(username);
            account.Password = password;
            _accountRepository.Update(account);
            _accountRepository.Save();
        }
        public void SetActiveAccount(string username)
        {
            var account = _accountRepository.GetAccountByUsernameThenActive(username);
            account.IsActive = true;
            account.Status = true;
            _accountRepository.Update(account);
            _accountRepository.Save();
        }
        public void InsertAccountByUser(AccountDTO accountDTO)
        {
            accountDTO.CreatedDate = DateTime.Now;
            accountDTO.IsDelete = false;
            accountDTO.Status = true;
            accountDTO.IsActive = false;
            var account = _mapper.Map<AccountDTO, Account>(accountDTO);
            _accountRepository.Insert(account);
            _accountRepository.Save();
        }
        public AccountDTO GetAccountByUsername(string username )
        {
            var account = _accountRepository.GetAccountByUsername(username );
            var accountDTO = _mapper.Map<AccountDTO>( account );
            return accountDTO;
        }
        public int CheckRegister(string Username,string Password,string Email)
        {
            var checkUsername = _accountRepository.CheckUsername(Username);
            var checkEmail = _accountRepository.CheckEmail(Email);
            if (checkUsername == true)
            {
                return 1;
            }
            else 
            {
                if (checkEmail == false)
                {
                    return 2;
                }
            }
            return 3;
        }
        public int CheckLogin(string Username,string Password)
        {
            var checkUsername = _accountRepository.CheckUsername(Username);
            var checkPassword = _accountRepository.CheckPassword(Password);
            if (checkUsername == true)
            {
                if (checkPassword == true)
                {
                    var account = _accountRepository.GetAccountByUsername(Username);
                    return 2 + account.AccountType;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 1;
            }
        }
        public IEnumerable<AccountDTO> SelectAll()
        {
            var accounts = _accountRepository.SelectAll();
            var accountDtos = accounts.Select(item => _mapper.Map<Account, AccountDTO>(item));
            return accountDtos;
        }
        public void DeleteAccount(long id)
        {
            var account = _accountRepository.SelectAccountById(id);
            _categoryProductBusiness.DeleteByAccountID(id);
            _productBusiness.DeleteByIDAccount(id);
            _commentBusiness.DeleteByIDAccount(id);
            _fileBusiness.DeleteByIDAccount(id);
            _orderBusiness.DeleteByIDAccount(id);
            _paymentBusiness.DeleteByIDAccount(id);
            _accountRepository.DeleteByItem(account);
            _accountRepository.Save();
           
        }
        public void EditAccount(AccountDTO accountDTO)
        {
            var account = _mapper.Map<AccountDTO, Account>(accountDTO);
            _accountRepository.Update(account);
            _accountRepository.Save();
        }
        public AccountDTO GetAccountById(long id)
        {
            var account = _accountRepository.GetAccountById(id);
            var accountDto = _mapper.Map<Account, AccountDTO>(account);
            return accountDto;
        }
        public void InsertAccount(AccountDTO accountDTO)
        {
            accountDTO.CreatedDate = DateTime.Now;
            accountDTO.IsActive = true;
            var account = _mapper.Map<AccountDTO, Account>(accountDTO);
            _accountRepository.Insert(account);
            _accountRepository.Save();
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
