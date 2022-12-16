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
        IEnumerable<OrderDTO> SelectByIDAccount(long IDAccount);
        long InsertOrder(OrderDTO orderDTO);
        void DeleteByIDAccount(long IDAccount);
        void DeleteByIDOrder(long IDOrder);
        void DeleteOrder(long id);
        long GetTotal();
        IEnumerable<OrderDTO> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<OrderDTO> SelectAll();
    }
}
