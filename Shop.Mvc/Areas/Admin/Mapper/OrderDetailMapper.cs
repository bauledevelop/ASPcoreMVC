using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Mapper
{
    public class OrderDetailMapper
    {
        public OrderDetailDTO MapperViewModelToDto(OrderDetailViewModel orderDetailViewModel)
        {
            var orderDetailDto = new OrderDetailDTO()
            {
                ID = orderDetailViewModel.ID,
                Quantity = orderDetailViewModel.Quantity,
                Total = orderDetailViewModel.Total,
                Status = orderDetailViewModel.Status,
                Size = orderDetailViewModel.Size,
                IDOrder = orderDetailViewModel.IDOrder,
                IDProduct = orderDetailViewModel.IDProduct,
            };
            return orderDetailDto;
        }
        public OrderDetailViewModel MapperDtoToViewModel(OrderDetailDTO orderDetailDTO)
        {
            var orderDetailViewModel = new OrderDetailViewModel()
            {
                ID = orderDetailDTO.ID,
                Quantity = orderDetailDTO.Quantity,
                Total = orderDetailDTO.Total,
                Status = orderDetailDTO.Status,
                Size = orderDetailDTO.Size,
                IDOrder = orderDetailDTO.IDOrder,
                IDProduct = orderDetailDTO.IDProduct,
            };
            return orderDetailViewModel;
        }
    }
}
