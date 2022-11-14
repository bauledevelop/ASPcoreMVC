using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces
{
    public interface IOrderBusiness
    {
        void DeleteOrder(long id);
        long GetTotal();
        IEnumerable<OrderDTO> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<OrderDTO> SelectAll();
    }
}
