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
    public class FeedbackBusiness : IFeedbackBusiness
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;
        public FeedbackBusiness(IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
        }
        public void DeleteFeedback(long id)
        {
            _feedbackRepository.Delete(id);
            _feedbackRepository.Save();
        }
        public long GetTotal()
        {
            return _feedbackRepository.GetTotal();
        }
        public IEnumerable<FeedbackDTO> SelectByQuantityItem(int page, int pageSize)
        {
            var feedbacks = _feedbackRepository.SelectByQuantityItem(page, pageSize);
            var feedbackDtos = feedbacks.Select(item => _mapper.Map<Feedback, FeedbackDTO>(item));
            return feedbackDtos;
        }
        public IEnumerable<FeedbackDTO> SelectAll()
        {
            var feedback = _feedbackRepository.SelectAll();
            var feedbackDTOs = feedback.Select(item => _mapper.Map<Feedback, FeedbackDTO>(item));
            return feedbackDTOs;
        }
    }
}
