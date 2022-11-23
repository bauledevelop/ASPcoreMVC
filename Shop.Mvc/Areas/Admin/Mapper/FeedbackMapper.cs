using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Mapper
{
    public class FeedbackMapper
    {
        public FeedbackDTO MapperViewModelToDto(FeedbackViewModel feedbackViewModel)
        {
            var feedbackDtos = new FeedbackDTO()
            {
                ID = feedbackViewModel.ID,
                Name = feedbackViewModel.Name,
                Content = feedbackViewModel.Content,
                CreatedDate = feedbackViewModel.CreatedDate,
                Status = feedbackViewModel.Status,
                IDAcount = feedbackViewModel.IDAcount
            };
            return feedbackDtos;
        }
        public FeedbackViewModel MapperDtoToViewModel(FeedbackDTO feedbackDto)
        {
            var feedbackViewModel = new FeedbackViewModel()
            {
                ID = feedbackDto.ID,
                Name = feedbackDto.Name,
                Content = feedbackDto.Content,
                CreatedDate = feedbackDto.CreatedDate,
                Status = feedbackDto.Status,
                IDAcount = feedbackDto.IDAcount
            };
            return feedbackViewModel;
        }
    }
}
