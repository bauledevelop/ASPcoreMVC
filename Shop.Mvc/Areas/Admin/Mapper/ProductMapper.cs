using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Mapper
{
    public class ProductMapper
    {
        public ProductDTO MapperViewModelToDto(ProductViewModel productViewModel)
        {
            var productDto = new ProductDTO()
            {
                ID = productViewModel.ID,
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                CreatedBy = productViewModel.CreatedBy,
                CreatedDate = productViewModel.CreatedDate,
                UpdatedDate = productViewModel.UpdatedDate,
                Status = productViewModel.Status,
                IDCategoryProduct = long.Parse(productViewModel.IDCategoryProduct),
            };
            return productDto;
        }
        public ProductViewModel MapperDtoToViewModel(ProductDTO productDTO)
        {
            var productViewModel = new ProductViewModel()
            {
                ID = productDTO.ID,
                Name = productDTO.Name,
                Description = productDTO.Description,
                CreatedBy = productDTO.CreatedBy,
                CreatedDate = productDTO.CreatedDate,
                UpdatedDate = productDTO.UpdatedDate,
                Status = productDTO.Status,
                IDCategoryProduct = productDTO.IDCategoryProduct.ToString(),
            };
            return productViewModel;
        }
    }
}
