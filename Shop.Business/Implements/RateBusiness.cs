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
    public class RateBusiness : IRateBusiness
    {
        private readonly IRateRepository _rateRepository;
        private readonly IMapper _mapper;
        public RateBusiness(IRateRepository rateRepository, IMapper mapper)
        {
            _rateRepository = rateRepository;
            _mapper = mapper;
        }

        public void Insert(RateDTO rateDTO)
        {
            var rate = _mapper.Map<RateDTO,Rate>(rateDTO);
            _rateRepository.Insert(rate);
            _rateRepository.Save();
        }
        public IEnumerable<RateDTO> SelectByIDProduct(long IDProduct)
        {
            var rates = _rateRepository.SelectByIDProduct(IDProduct);
            if (rates == null)
                return null;
            var rateDTOs = rates.Select(item => _mapper.Map<Rate, RateDTO>(item));
            return rateDTOs;
        }
        public IEnumerable<RateDTO> GetRateByIDAccount(long idAccount)
        {
            var rate = _rateRepository.GetRateByIDAccount(idAccount);
            var rateDTO = rate.Select(item => _mapper.Map<Rate, RateDTO>(item));
            return rateDTO;
        }
    }
}
