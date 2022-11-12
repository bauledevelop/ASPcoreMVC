using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Mapper
{
    public class CategoryMapper
    {
        public CategoryProductViewModel MapperDtoToViewModel(CategoryProductDTO categoryProductDTO)
        {
            var categoryViewModel = new CategoryProductViewModel()
            {
                ID = categoryProductDTO.ID,
                Name = categoryProductDTO.Name,
                CreatedDate = categoryProductDTO.CreatedDate,
                CreatedBy = categoryProductDTO.CreatedBy,
                UpdateDate = categoryProductDTO.UpdateDate,
                Status = categoryProductDTO.Status,
            };
            return categoryViewModel;
        }
        public CategoryProductDTO MapperViewModelToDto(CategoryProductViewModel categoryProductViewModel)
        {
            var categoryDto = new CategoryProductDTO()
            {
                ID = categoryProductViewModel.ID,
                Name = categoryProductViewModel.Name,
                CreatedDate = categoryProductViewModel.CreatedDate,
                CreatedBy = categoryProductViewModel.CreatedBy,
                UpdateDate = categoryProductViewModel.UpdateDate,
                Status = categoryProductViewModel.Status,
            };
            return categoryDto;
        }
    }
}
