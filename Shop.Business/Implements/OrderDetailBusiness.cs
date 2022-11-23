using Shop.Business.Interfaces;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Implements
{
    public class OrderDetailBusiness : IOrderDetailBusiness
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderDetailBusiness(IOrderDetailRepository orderDetailRepository )
        {
            _orderDetailRepository = orderDetailRepository;
        }
        public void DeleteOrderDetail(long ID)
        {
            _orderDetailRepository.Delete(ID);
            _orderDetailRepository.Save();
        }
        public void DeleteByIDOrder(long IDOrder)
        {
            var orderDetails = _orderDetailRepository.SelectAll();
            foreach (var item in orderDetails)
            {
                if (item.IDOrder == IDOrder)
                {
                    _orderDetailRepository.DeleteByItem(item);
                    _orderDetailRepository.Save();
                }
            }
        }
        public void DeleteByIDProduct(long IDProduct)
        {
            var orderDetails = _orderDetailRepository.SelectAll();
            if (orderDetails != null)
            {
                foreach (var item in orderDetails)
                {
                    if (item.IDProduct == IDProduct)
                    {
                        _orderDetailRepository.DeleteByItem(item);
                        _orderDetailRepository.Save();
                    }
                }
            }
        }
    }
}
