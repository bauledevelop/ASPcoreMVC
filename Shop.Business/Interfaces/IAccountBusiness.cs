﻿using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces
{
    public interface IAccountBusiness
    {
        IEnumerable<AccountDTO> SelectAll();
        void DeleteAccount(long id);
        bool EditAccount(AccountDTO accountDTO);
        AccountDTO GetAccountById(long id);
        bool InsertAccount(AccountDTO accountDTO);
        long GetTotal();
        IEnumerable<AccountDTO> SelectByQuantityItem(int page, int pageSize);
    }
}
