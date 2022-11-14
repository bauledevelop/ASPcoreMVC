using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Mapper;
using Shop.Mvc.Areas.Admin.Models;
using Shop.Mvc.Commons.DropdownList;
using Shop.Mvc.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductBusiness _productBusiness;
        private readonly IAccountBusiness _accountBusiness;
        private readonly ICategoryProductBusiness _categoryProductBusiness;
        public ProductController(IProductBusiness productBusiness,
            IAccountBusiness accountBusiness, ICategoryProductBusiness categoryProductBusiness)
        {
            _productBusiness = productBusiness;
            _accountBusiness = accountBusiness;
            _categoryProductBusiness = categoryProductBusiness;
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Index(int page=1,int pageSize=1)
        {
            try
            {
                var model = _productBusiness.SelectByQuantityItem(page, pageSize);
                var productViewModels = new List<ProductViewModel>();
                var mapperProduct = new ProductMapper();
                var mapperAccount = new AccountMapper();
                var mapperCategory = new CategoryMapper();
                var pagination = new PaginationModel();
                var total = _productBusiness.GetTotal();
                var listAccount = _accountBusiness.SelectAll();
                var listCategory = _categoryProductBusiness.SelectAllCategory();
                var categoryViewModel = new List<CategoryProductViewModel>();
                var accountViewModel = new List<AccountViewModel>();
                if (model != null)
                {
                    foreach (var item in listAccount)
                    {
                        accountViewModel.Add(mapperAccount.MapperDtoToViewModel(item));
                    }
                    foreach (var item in model)
                    {
                        productViewModels.Add(mapperProduct.MapperDtoToViewModel(item));
                    }
                    foreach(var item in productViewModels)
                    {
                        item.IDCategoryProduct = listCategory.SingleOrDefault(child => child.ID == long.Parse(item.IDCategoryProduct)).Name;
                    }
                    pagination.Total = total;
                    pagination.Show = (total != 0 ? ((page - 1) * pageSize) + 1 : 0);
                    pagination.ShowTo = (((page - 1) * pageSize) + 1) + model.Count() - 1;
                    pagination.Page = page;
                    int maxPage = 5;
                    int totalPage = 0;
                    totalPage = (int)Math.Ceiling((double)((double)total / (double)pageSize));
                    pagination.TotalPage = totalPage;
                    pagination.MaxPage = 5;
                    pagination.First = 1;
                    pagination.Last = totalPage;
                    pagination.Next = page + 1;
                    pagination.Prev = page - 1;
                    ViewData["ListAccount"] = accountViewModel;
                    ViewData["Pagination"] = pagination;
                }
                return View(productViewModels);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                _productBusiness.DeleteProduct(long.Parse(id));
                return Json(new
                {
                    status = true
                });
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var productDto = _productBusiness.SelectById(long.Parse(id));
                var categoryDto = _categoryProductBusiness.SelectAllCategory();
                var getCategoryDto = _categoryProductBusiness.GetCategoryById(productDto.IDCategoryProduct);
                var mapperProduct = new ProductMapper();
                var mapperCategory = new CategoryMapper();
                var model = new ProductViewModel();
                var dropdownList = new DropdownListItem();
                model = mapperProduct.MapperDtoToViewModel(productDto);
                ViewData["ListCategory"] = new SelectList(dropdownList.DropdownListCategoryActive(categoryDto, productDto.IDCategoryProduct), "Value", "Text");
                return View(model);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            var dropdownList = new DropdownListItem();
            var categoryDto = _categoryProductBusiness.SelectAllCategory();
            ViewData["ListCategory"] = new SelectList(dropdownList.DropdownListCategoryActive(categoryDto, long.Parse(productViewModel.IDCategoryProduct)));
            if (!ModelState.IsValid) return View(productViewModel);
            try
            {
                var mapperProduct = new ProductMapper();
                var productDto = mapperProduct.MapperViewModelToDto(productViewModel);
                _productBusiness.EditProduct(productDto);
                return Redirect("/Admin/Product");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Thay đổi sản phẩm thất bại";
            }
            return View(productViewModel);
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var categoryProducts = _categoryProductBusiness.SelectAllCategory();
            var dropdownList = new DropdownListItem();
            ViewData["ListCategory"] = new SelectList(dropdownList.DropdownListCategory(categoryProducts), "Value", "Text");
            return View();
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            var dropdownList = new DropdownListItem();
            var categoryDto = _categoryProductBusiness.SelectAllCategory();
            ViewData["ListCategory"] = new SelectList(dropdownList.DropdownListCategoryActive(categoryDto, long.Parse(productViewModel.IDCategoryProduct)));
            if (!ModelState.IsValid) return View(productViewModel);
            try
            {
                var mapperProduct = new ProductMapper();
                productViewModel.CreatedBy = 1;
                var productDto = mapperProduct.MapperViewModelToDto(productViewModel);
                _productBusiness.CreateProduct(productDto);
                return Redirect("/Admin/Product");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Tạo sản phẩm thất bại";
            }
            return View(productViewModel);
        }
    }
}
