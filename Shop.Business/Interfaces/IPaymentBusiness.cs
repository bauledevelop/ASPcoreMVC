using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces
{
    public interface IPaymentBusiness
    {
        void DeleteOrder(long id);
        long GetTotal();
        IEnumerable<PaymentDTO> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<PaymentDTO> SelectAll();
    }
}
