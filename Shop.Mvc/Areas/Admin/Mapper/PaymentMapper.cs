using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Mapper
{
    public class PaymentMapper
    {
        public PaymentDTO MapperViewModelToDto(PaymentViewModel paymentViewModel)
        {
            var paymentDTO = new PaymentDTO()
            {
                ID = paymentViewModel.ID,
                Email = paymentViewModel.Email,
                Phone = paymentViewModel.Phone,
                CreatedDate = paymentViewModel.CreatedDate,
                Status = paymentViewModel.Status,
                IDAccount = paymentViewModel.IDAccount,
                IDOrder = paymentViewModel.IDOrder,
            };
            return paymentDTO;
        }
        public PaymentViewModel MapperDtoToViewModel(PaymentDTO paymentDTO)
        {
            var paymentViewModel = new PaymentViewModel()
            {
                ID = paymentDTO.ID,
                Email = paymentDTO.Email,
                Phone = paymentDTO.Phone,
                CreatedDate = paymentDTO.CreatedDate,
                Status = paymentDTO.Status,
                IDAccount = paymentDTO.IDAccount,
                IDOrder = paymentDTO.IDOrder,
            };
            return paymentViewModel;
        }
    }
}
