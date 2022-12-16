using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Mapper
{
    public class AccountMapper
    {
        public AccountDTO MapperViewModelToDTO(AccountViewModel accountViewModel)
        {
            var accountDto = new AccountDTO()
            {
                ID = accountViewModel.ID,
                Username = accountViewModel.Username,
                Password = accountViewModel.Password,
                Name = accountViewModel.Name,
                BirthDay = accountViewModel.BirthDay,
                Sex = int.Parse(accountViewModel.Sex),
                Address = accountViewModel.Address,
                Phone = accountViewModel.Phone,
                Email = accountViewModel.Email,
                CreatedDate = accountViewModel.CreatedDate,
                AccountType = int.Parse(accountViewModel.AccountType),
                Status = accountViewModel.Status
            };
            return accountDto;
        }
        public AccountViewModel MapperDtoToViewModel(AccountDTO accountDTO)
        {
            var accountViewModel = new AccountViewModel()
            {
                ID = accountDTO.ID,
                Username = accountDTO.Username,
                Password = accountDTO.Password,
                Name = accountDTO.Name,
                BirthDay = accountDTO.BirthDay,
                Sex = accountDTO.Sex.ToString(),
                Address = accountDTO.Address,
                Phone = accountDTO.Phone,
                Email = accountDTO.Email,
                CreatedDate = accountDTO.CreatedDate,
                AccountType = accountDTO.AccountType.ToString(),
                Status = accountDTO.Status
            };
            return accountViewModel;
        }
    }
}
