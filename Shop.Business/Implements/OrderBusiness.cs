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
        public OrderBusiness(IOrderRepository orderRepository, IMapper mapper,IOrderDetailBusiness orderDetailBusiness)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderDetailBusiness = orderDetailBusiness;
        }
        public long RevenueMonth(int month)
        {
            var model = _orderRepository.GetTotalMonth(month);
            long result = 0;
            foreach(var item in model)
            {
                result += item.Total;
            }
            return result;
        }
        public long TotalOrder()
        {
            var order = _orderRepository.SelectAll();
            long total = 0;
            foreach(var item in order)
            {
                total += item.Total;
            }
            return total;
        }
        public long CountOrder()
        {
            return _orderRepository.SelectAll().Count();
        }
        public void ChangeStatus(long id)
        {
            var order = _orderRepository.SelectById(id);
            order.Status = (order.Status == true) ? false : true;
            _orderRepository.Update(order);
            _orderRepository.Save();
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
            orderDTO.Status = false;
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
