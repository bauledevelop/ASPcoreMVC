using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Mapper
{
    public class MenuMapper
    {
        public MenuDTO MapperViewModelToDto(MenuViewModel menuViewModel)
        {
            var menuDTO = new MenuDTO()
            {
                ID = menuViewModel.ID,
                Name = menuViewModel.Name,
                CreatedDate = menuViewModel.CreatedDate,
                UpdatedDate = menuViewModel.UpdatedDate,
                CreatedBy = menuViewModel.CreatedBy,
                Status = menuViewModel.Status,
            };
            return menuDTO;
        }
        public MenuViewModel MapperDtoToViewModel(MenuDTO menuDTO)
        {
            var menuViewModel = new MenuViewModel()
            {
                ID = menuDTO.ID,
                Name = menuDTO.Name,
                CreatedDate = menuDTO.CreatedDate,
                UpdatedDate = menuDTO.UpdatedDate,
                CreatedBy = menuDTO.CreatedBy,
                Status = menuDTO.Status,
            };
            return menuViewModel;
        }
    }
}
