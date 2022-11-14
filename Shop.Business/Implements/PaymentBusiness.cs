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
    public class PaymentBusiness : IPaymentBusiness
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public PaymentBusiness(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }
        public void DeleteOrder(long id)
        {
            _paymentRepository.Delete(id);
            _paymentRepository.Save();
        }
        public long GetTotal()
        {
            return _paymentRepository.GetTotal();
        }
        public IEnumerable<PaymentDTO> SelectByQuantityItem(int page, int pageSize)
        {
            var payments = _paymentRepository.SelectByQuantityItem(page, pageSize);
            var paymentDtos = payments.Select(item => _mapper.Map<Payment, PaymentDTO>(item));
            return paymentDtos;
        }
        public IEnumerable<PaymentDTO> SelectAll()
        {
            var orders = _paymentRepository.SelectAll();
            var orderDtos = orders.Select(item => _mapper.Map<Payment, PaymentDTO>(item));
            return orderDtos;
        }
    }
}
