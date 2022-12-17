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
    public class OrderDetailBusiness : IOrderDetailBusiness
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        public OrderDetailBusiness(IOrderDetailRepository orderDetailRepository, IMapper mapper )
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }
        public IEnumerable<OrderDetailDTO> SelectByIDOrder(long IDOrder)
        {
            var orderDetail = _orderDetailRepository.SelectByIDOrder(IDOrder);
            var orderDetailDTO = orderDetail.Select(item => _mapper.Map<OrderDetail, OrderDetailDTO>(item));
            return orderDetailDTO;
        }
        public long GetAmountByIDProduct(long id)
        {
            var orderDetails = _orderDetailRepository.SelectByIDProduct(id);
            long total = 0;
            if (orderDetails == null)
                return 0;
            foreach(var item in orderDetails)
            {
                total += item.Quantity;
            }
            return total;
        }
        public void InsertOrderDetail(OrderDetailDTO orderDetailDTO)
        {
            orderDetailDTO.Status = true;
            var orderDetail = _mapper.Map<OrderDetailDTO, OrderDetail>(orderDetailDTO);
            _orderDetailRepository.Insert(orderDetail);
            _orderDetailRepository.Save();
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
