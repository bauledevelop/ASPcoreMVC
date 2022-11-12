using AutoMapper;
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
