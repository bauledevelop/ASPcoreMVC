using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Mapper
{
    public class OrderMapper
    {
        public OrderDTO MapperViewModelToDto(OrderViewModel orderViewModel)
        {
            var orderDto = new OrderDTO()
            {
                ID = orderViewModel.ID,
                Quantity = orderViewModel.Quantity,
                Total = orderViewModel.Total,
                CreatedDate = orderViewModel.CreatedDate,
                Status = orderViewModel.Status,
                IDAccount = orderViewModel.IDAccount,
            };
            return orderDto;
        }
        public OrderViewModel MapperDtoToViewModel(OrderDTO orderDTO)
        {
            var orderViewModel = new OrderViewModel()
            {
                ID = orderDTO.ID,
                Quantity = orderDTO.Quantity,
                Total = orderDTO.Total,
                CreatedDate = orderDTO.CreatedDate,
                Status = orderDTO.Status,
                IDAccount = orderDTO.IDAccount,
            };
            return orderViewModel;
        }
    }
}
