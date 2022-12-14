using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Mapper
{
    public class FileMapper
    {
        public FileDTO MapperViewModelToDto(FileViewModel fileViewModel)
        {
            var fileDto = new FileDTO()
            {
                ID = fileViewModel.ID,
                FileContent = fileViewModel.FileContent,
                Type = int.Parse(fileViewModel.Type),
                CreatedDate = fileViewModel.CreatedDate,
                UpdatedDate = fileViewModel.UpdatedDate,
                Status = fileViewModel.Status,
                CreatedBy = fileViewModel.CreatedBy,
                IDProduct = long.Parse(fileViewModel.IDProduct)
            };
            return fileDto;
        }
        public FileViewModel MapperDtoToViewModel(FileDTO fileDTO)
        {
            var fileViewModel = new FileViewModel()
            {
                ID = fileDTO.ID,
                FileContent = fileDTO.FileContent,
                Type = fileDTO.Type.ToString(),
                CreatedDate = fileDTO.CreatedDate,
                UpdatedDate = fileDTO.UpdatedDate,
                Status = fileDTO.Status,
                CreatedBy = fileDTO.CreatedBy,
                IDProduct = fileDTO.IDProduct.ToString()
            };
            return fileViewModel;
        }
    }
}
