using AutoMapper;
using Business.Tool;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Entities.Enities;
using Shop.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Implements
{
    public class CategoryProductBusiness : ICategoryProductBusiness
    {
        private readonly ICategoryProductRepository _categoryProductRepository;
        private readonly IMapper _mapper;
        private readonly IProductBusiness _productBusiness;
        public CategoryProductBusiness(ICategoryProductRepository accountRepository, IMapper mapper,IProductBusiness productBusiness)
        {
            _categoryProductRepository = accountRepository;
            _mapper = mapper;
            _productBusiness = productBusiness;
        }
        public IEnumerable<CategoryProductDTO> SelectByIDMenu(long IDMenu)
        {
            var category = _categoryProductRepository.SelectByIDMenu(IDMenu);
            var categoryDtos = category.Select(item => _mapper.Map<CategoryProduct, CategoryProductDTO>(item));
            return categoryDtos;
        }
        public IEnumerable<CategoryProductDTO> SelectAllCategoryByStatus()
        {
            var categorys = _categoryProductRepository.SelectAllByStatus();
            var categoryDtos = categorys.Select(item => _mapper.Map<CategoryProduct, CategoryProductDTO>(item));
            return categoryDtos;
        }
        public IEnumerable<CategoryProductDTO> SelectAllCategory()
        {
            var categorys = _categoryProductRepository.SelectAllByDelete();
            var categoryDtos = categorys.Select(item => _mapper.Map<CategoryProduct, CategoryProductDTO>(item));
            return categoryDtos;
        }
        public void DeleteByMenuID(long IDMenu)
        {
            var category = _categoryProductRepository.SelectAll();
            foreach (var item in category)
            {
                if (item.IDMenu == IDMenu)
                {
                    _productBusiness.DeleteByCategoryID(item.ID);
                    _categoryProductRepository.Delete(item.ID);
                    _categoryProductRepository.Save();
                }
            }
        }
        public void DeleteByAccountID(long IDAccount)
        {
            var category = _categoryProductRepository.SelectAll();
            foreach(var item in category)
            {
                if (item.CreatedBy == IDAccount)
                {
                    _productBusiness.DeleteByCategoryID(item.ID);
                    _categoryProductRepository.Delete(item.ID);
                    _categoryProductRepository.Save();
                }
            }
        }
        public void DeleteCategory(long id)
        {
            var category = _categoryProductRepository.SelectById(id);
            _productBusiness.DeleteByCategoryID(id);
            _categoryProductRepository.Delete(id);
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
