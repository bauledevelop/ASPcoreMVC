using AutoMapper;
using Business.Tool;
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
    public class CategoryProductBusiness : ICategoryProductBusiness
    {
        private readonly ICategoryProductRepository _categoryProductRepository;
        private readonly IMapper _mapper;
        public CategoryProductBusiness(ICategoryProductRepository accountRepository, IMapper mapper)
        {
            _categoryProductRepository = accountRepository;
            _mapper = mapper;
        }
        public IEnumerable<CategoryProductDTO> SelectAllCategory()
        {
            var categorys = _categoryProductRepository.SelectAllByDelete();
            var categoryDtos = categorys.Select(item => _mapper.Map<CategoryProduct, CategoryProductDTO>(item));
            return categoryDtos;
        }
        public void DeleteCategory(long id)
        {
            var category = _categoryProductRepository.SelectById(id);
            category.IsDelete = true;
            _categoryProductRepository.Update(category);
            _categoryProductRepository.Save();
        }
        public bool EditCategory(CategoryProductDTO categoryDto)
        {
            var category = _mapper.Map<CategoryProductDTO, CategoryProduct>(categoryDto);
            category.UpdateDate = DateTime.Now;
            _categoryProductRepository.Update(category);
            _categoryProductRepository.Save();
            return true;
        }
        public CategoryProductDTO GetCategoryById(long id)
        {
            var category = _categoryProductRepository.SelectById(id);
            var categoryDto = _mapper.Map<CategoryProduct, CategoryProductDTO>(category);
            return categoryDto;
        }
        public bool InsertCategory(CategoryProductDTO categoryProductDTO)
        {
            var category = _mapper.Map<CategoryProductDTO, CategoryProduct>(categoryProductDTO);
            category.CreatedDate = DateTime.Now;
            category.UpdateDate = DateTime.Now;
            category.IsDelete = false;
            category.CreatedBy = 1;
            _categoryProductRepository.Insert(category);
            _categoryProductRepository.Save();
            return true;
        }
        public IEnumerable<CategoryProductDTO> SelectByQuantityItem(int page, int pageSize)
        {
            var categorys = _categoryProductRepository.SelectByQuantityItem(page, pageSize);
            var categoryDtos = categorys.Select(item => _mapper.Map<CategoryProduct, CategoryProductDTO>(item));
            return categoryDtos;
        }
        public long GetTotal()
        {
            return _categoryProductRepository.GetTotal();
        }
    }
}
