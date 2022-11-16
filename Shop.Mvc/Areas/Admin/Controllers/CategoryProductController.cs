using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Business.Interfaces;
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
    public class CategoryProductController : Controller
    {
        private readonly IAccountBusiness _accountBusiness;
        private readonly ICategoryProductBusiness _categoryProductBusiness;
        private readonly IMenuBusiness _menuBusiness;
        public CategoryProductController(ICategoryProductBusiness categoryProductBusiness,
            IAccountBusiness accountBusiness,
            IMenuBusiness menuBusiness
            )
        {
            _categoryProductBusiness = categoryProductBusiness;
            _accountBusiness = accountBusiness;
            _menuBusiness = menuBusiness;
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Index(int page=1,int pageSize=1)
        {
            try
            {
                var model = _categoryProductBusiness.SelectByQuantityItem(page, pageSize);
                var categoryViewModels = new List<CategoryProductViewModel>();
                var mapperCategory = new CategoryMapper();
                var mapperAccount = new AccountMapper();
                var pagination = new PaginationModel();
                var total = _categoryProductBusiness.GetTotal();
                var listAccount = _accountBusiness.SelectAll();
                var listMenu = _menuBusiness.SelectAll();
                var accountViewModel = new List<AccountViewModel>();
                if (model != null)
                {
                    foreach (var item in listAccount)
                    {
                        accountViewModel.Add(mapperAccount.MapperDtoToViewModel(item));
                    }
                    foreach (var item in model)
                    {
                        categoryViewModels.Add(mapperCategory.MapperDtoToViewModel(item));
                    }
                    foreach(var item in categoryViewModels)
                    {
                        foreach(var child in listMenu)
                        {
                            if (long.Parse(item.IDMenu) == child.ID)
                            {
                                item.IDMenu = child.Name;
                                break;
                            }
                        }
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
                return View(categoryViewModels);
            }
            catch(Exception ex)
            {

            }
            return View();
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            _categoryProductBusiness.DeleteCategory(long.Parse(id));
            return Json(new
            {
                status = true
            });
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var dropdownList = new DropdownListItem();
            var listMenu = _menuBusiness.SelectAll();
            ViewData["ListMenu"] = new SelectList(dropdownList.DropdownListMenu(listMenu), "Value", "Text");
            return View();
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Create(CategoryProductViewModel categoryProductViewModel)
        {
            var dropdownList = new DropdownListItem();
            var listMenu = _menuBusiness.SelectAll();
            ViewData["ListMenu"] = new SelectList(dropdownList.DropdownListMenuActive(listMenu,long.Parse(categoryProductViewModel.IDMenu)), "Value", "Text");
            if (!ModelState.IsValid) return View(categoryProductViewModel);
            try
            {
                var mapperCategory = new CategoryMapper();
                var categoryDto = mapperCategory.MapperViewModelToDto(categoryProductViewModel);
                _categoryProductBusiness.InsertCategory(categoryDto);
                return Redirect("/Admin/CategoryProduct");
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Thêm Loại sản phẩm không thành công";
            }
            return View(categoryProductViewModel);
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            
            try
            {
                var mapperCategory = new CategoryMapper();
                var category = _categoryProductBusiness.GetCategoryById(long.Parse(id));
                var categoryViewModel = mapperCategory.MapperDtoToViewModel(category);
                var dropdownList = new DropdownListItem();
                var listMenu = _menuBusiness.SelectAll();
                ViewData["ListMenu"] = new SelectList(dropdownList.DropdownListMenuActive(listMenu, long.Parse(categoryViewModel.IDMenu)), "Value", "Text");
                return View(categoryViewModel);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Edit(CategoryProductViewModel categoryProductViewModel)
        {
            var dropdownList = new DropdownListItem();
            var listMenu = _menuBusiness.SelectAll();
            ViewData["ListMenu"] = new SelectList(dropdownList.DropdownListMenuActive(listMenu, long.Parse(categoryProductViewModel.IDMenu)), "Value", "Text");
            if (!ModelState.IsValid) return View(categoryProductViewModel);
            try
            {
                var mapperCategory = new CategoryMapper();
                var category = mapperCategory.MapperViewModelToDto(categoryProductViewModel);
                _categoryProductBusiness.EditCategory(category);
                return Redirect("/Admin/CategoryProduct");
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Chỉnh sửa loại sản phẩm thất bại";
            }
            return View(categoryProductViewModel);
        }
    }
}
