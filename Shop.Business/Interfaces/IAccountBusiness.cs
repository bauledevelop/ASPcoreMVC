using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces
{
    public interface IAccountBusiness
    {
        long GetTotal();
        IEnumerable<AccountDTO> SelectByQuantityItem(int page, int pageSize);
    }
}
