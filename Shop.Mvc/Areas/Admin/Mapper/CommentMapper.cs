using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Mapper
{
    public class CommentMapper
    {
        public CommentDTO MapperViewModelToDto(CommentViewModel commentViewModel)
        {
            var commentDto = new CommentDTO()
            {
                ID = commentViewModel.ID,
                Content = commentViewModel.Content,
                CreatedDate = commentViewModel.CreatedDate,
                Status = commentViewModel.Status,
                IDAccount = commentViewModel.IDAccount,
                IDProduct = commentViewModel.IDProduct
            };
            return commentDto;
        }
        public CommentViewModel MapperDtoToViewModel(CommentDTO commentDTO)
        {
            var commentViewModel = new CommentViewModel()
            {
                ID = commentDTO.ID,
                Content = commentDTO.Content,
                CreatedDate = commentDTO.CreatedDate,
                Status = commentDTO.Status,
                IDAccount = commentDTO.IDAccount,
                IDProduct = commentDTO.IDProduct
            };
            return commentViewModel;
        }
    }
}
