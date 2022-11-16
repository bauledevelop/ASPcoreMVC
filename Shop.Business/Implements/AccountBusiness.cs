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
        private readonly IFeedbackBusiness _feedbackBusiness;
        private readonly IFileBusiness _fileBusiness;
        private readonly ICommentBusiness _commentBusiness;
        private readonly IPaymentBusiness _paymentBusiness;
        private readonly IOrderBusiness _orderBusiness;
        public AccountBusiness(IAccountRepository accountRepository,IMapper mapper,
            ICategoryProductBusiness categoryProductBusiness,
            IProductBusiness productBusiness,
            IFeedbackBusiness feedbackBusiness,
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
            _feedbackBusiness = feedbackBusiness;
            _fileBusiness = fileBusiness;
            _commentBusiness = commentBusiness;
            _paymentBusiness = paymentBusiness;
            _orderBusiness = orderBusiness;
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
            _feedbackBusiness.DeleteByIDAccount(id);
            _fileBusiness.DeleteByIDAccount(id);
            _orderBusiness.DeleteByIDAccount(id);
            _paymentBusiness.DeleteByIDAccount(id);
            _accountRepository.DeleteByItem(account);
            _accountRepository.Save();
           
        }
        public bool EditAccount(AccountDTO accountDTO)
        {
            var account = _mapper.Map<AccountDTO, Account>(accountDTO);
            var SHA1 = new HashPasswordSHA1();
            var phone = _accountRepository.GetPhoneByUsername(account.Username);
            var check = _accountRepository.CheckPhone(account.Phone);
            if (check || phone == accountDTO.Phone)
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
            var account = _accountRepository.GetAccountById(id);
            var accountDto = _mapper.Map<Account, AccountDTO>(account);
            return accountDto;
        }
        public int InsertAccount(AccountDTO accountDTO)
        {
            var account = _mapper.Map<AccountDTO, Account>(accountDTO);
            var SHA1 = new HashPasswordSHA1();
            account.CreatedDate = DateTime.Now;
            account.IsActive = true;
            account.IsDelete = false;
            var checkEmail = _accountRepository.CheckEmail(account.Email);
            var checkPhone = _accountRepository.CheckPhone(account.Phone);
            if (checkEmail)
            {
                if (checkPhone)
                {
                    account.Password = SHA1.HashPassword(account.Password);
                    _accountRepository.Insert(account);
                    _accountRepository.Save();
                    return 1;
                }
                return 3;
            }
            else
            {
                return 2;
            }
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
