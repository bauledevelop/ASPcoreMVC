using Shop.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Interfaces
{
    public interface IOrderDetailBusiness
    {

        long GetAmountByIDProduct(long id);
        void InsertOrderDetail(OrderDetailDTO orderDetailDTO);
        void DeleteByIDOrder(long IDOrder);
        void DeleteByIDProduct(long IDProduct);
    }
}
