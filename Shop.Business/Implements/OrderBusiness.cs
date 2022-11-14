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
        public OrderBusiness(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public void DeleteOrder(long id)
        {
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
