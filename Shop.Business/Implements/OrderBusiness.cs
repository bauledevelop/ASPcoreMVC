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
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IOrderDetailBusiness _orderDetailBusiness;
        private readonly IPaymentBusiness _paymentBusiness;
        public OrderBusiness(IOrderRepository orderRepository, IMapper mapper,IOrderDetailBusiness orderDetailBusiness,IPaymentBusiness paymentBusiness)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderDetailBusiness = orderDetailBusiness;
            _paymentBusiness = paymentBusiness;
        }
        public IEnumerable<OrderDTO> SelectByIDAccount(long IDAccount)
        {
            var order = _orderRepository.SelectByIDAccount(IDAccount);
            var orderDTOs = order.Select(x => _mapper.Map<Order, OrderDTO>(x));
            return orderDTOs;
        }
        public long InsertOrder(OrderDTO orderDTO)
        {
            orderDTO.CreatedDate = DateTime.Now;
            orderDTO.IsDelete = false;
            orderDTO.Status = true;
            var order = _mapper.Map<OrderDTO, Order>(orderDTO);
            long _id = _orderRepository.InsertOrder(order);
            return _id;
        }
        public void DeleteByIDAccount(long IDAccount)
        {
            var orders = _orderRepository.SelectAll();
            foreach(var item in orders)
            {
                if (item.IDAccount == IDAccount)
                {
                    _orderDetailBusiness.DeleteByIDOrder(item.ID);
                    _orderRepository.DeleteByItem(item);
                }
            }
        }
        public void DeleteByIDOrder(long IDOrder)
        {
            _orderRepository.Delete(IDOrder);
            _orderRepository.Save();
        }
        public void DeleteOrder(long id)
        {
            _orderDetailBusiness.DeleteByIDOrder(id);
            _paymentBusiness.DeleteByIDOrder(id);
            _orderRepository.Delete(id);
            _orderRepository.Save();
        }
        public long GetTotal()
        {
            return _orderRepository.GetTotal();
        }
        public IEnumerable<OrderDTO> SelectByQuantityItem(int page, int pageSize)
        {
            var orders = _orderRepository.SelectByQuantityItem(page, pageSize);
            var orderDtos = orders.Select(item => _mapper.Map<Order, OrderDTO>(item));
            return orderDtos;
        }
        public IEnumerable<OrderDTO> SelectAll()
        {
            var orders = _orderRepository.SelectAll();
            var orderDtos = orders.Select(item => _mapper.Map<Order, OrderDTO>(item));
            return orderDtos;
        }
    }
}
