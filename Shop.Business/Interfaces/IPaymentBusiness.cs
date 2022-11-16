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
        void DeletePayment(long ID);
        void DeleteByIDOrder(long IDOrder);
        void DeleteByIDAccount(long IDAccount);
        long GetTotal();
        IEnumerable<PaymentDTO> SelectByQuantityItem(int page, int pageSize);
        IEnumerable<PaymentDTO> SelectAll();
    }
}
