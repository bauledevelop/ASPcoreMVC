using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Business.Implements;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Entities.Enities;
using Shop.Mvc.Areas.Admin.Mapper;
using Shop.Mvc.Areas.Admin.Models;
using Shop.Mvc.Commons.DropdownList;
using Shop.Mvc.Commons.Models;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller
    {
        private readonly IProductBusiness _productBusiness;
        private readonly IAccountBusiness _accountBusiness;
        private readonly ICategoryProductBusiness _categoryProductBusiness;
        private readonly IMenuBusiness _menuBusiness;
        private readonly IFileBusiness _fileBusiness;
        public ProductController(IProductBusiness productBusiness,
            IAccountBusiness accountBusiness, ICategoryProductBusiness categoryProductBusiness, IMenuBusiness menuBusiness,
            IFileBusiness fileBusiness)
        {
            _productBusiness = productBusiness;
            _accountBusiness = accountBusiness;
            _categoryProductBusiness = categoryProductBusiness;
            _menuBusiness = menuBusiness;
            _fileBusiness = fileBusiness;
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Index(int page=1,int pageSize=5)
        {
            try
            {
                var model = _productBusiness.SelectByQuantityItem(page, pageSize);
                var productViewModels = new List<ProductViewModel>();
                var mapperProduct = new ProductMapper();
                var mapperAccount = new AccountMapper();
                var mapperCategory = new CategoryMapper();
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
                    ViewData["ListAccount"] = accountViewModel;
                    ViewData["Pagination"] = _productBusiness.SelectAll().ToPagedList(page,pageSize);
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
        public async Task<IActionResult> Delete(string id)
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
                var getCategoryDto = _categoryProductBusiness.GetCategoryById(productDto.IDCategoryProduct);
                var categoryDto = _categoryProductBusiness.SelectByIDMenu(getCategoryDto.IDMenu);
                var mapperProduct = new ProductMapper();
                var mapperCategory = new CategoryMapper();
                var model = new ProductViewModel();
                var dropdownList = new DropdownListItem();
                model = mapperProduct.MapperDtoToViewModel(productDto);
                ViewData["ListCategory"] = new SelectList(dropdownList.DropdownListCategoryActive(categoryDto, productDto.IDCategoryProduct), "Value", "Text");
                model.IDMenu = getCategoryDto.IDMenu.ToString();
                return View(model);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel productViewModel,List<IFormFile> uploadFiles)
        {
            var categoryDto = _categoryProductBusiness.SelectByIDMenu(long.Parse(productViewModel.IDMenu));
            var dropdownList = new DropdownListItem();
            ViewData["ListCategory"] = new SelectList(dropdownList.DropdownListCategoryActive(categoryDto, long.Parse(productViewModel.IDCategoryProduct)),"Value","Text");
            if (!ModelState.IsValid) return View(productViewModel);
            try
            {
                if (uploadFiles.Count > 0)
                {
                    var accountDTO = _accountBusiness.GetAccountByUsername(User.Identity.Name);
                    var listFile = _fileBusiness.SelectByIDProduct(productViewModel.ID);
                    if (listFile != null)
                    {
                        foreach (FileDTO item in listFile)
                        {
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//uploadFiles", item.FileContent);
                            System.IO.File.Delete(path);
                        }
                    }
                    var fileDTO = new FileDTO();
                    fileDTO.Type = 1;
                    fileDTO.CreatedBy = accountDTO.ID;
                    fileDTO.IDProduct = productViewModel.ID;
                    fileDTO.Status = true;
                    _fileBusiness.DeleteFileByIDProduct(productViewModel.ID);
                    foreach (var file in uploadFiles)
                    {
                        string fileName = file.FileName;
                        fileName = Path.GetFileName(fileName);
                        string uploadPaths = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//uploadFiles", fileName);
                        var stream = new FileStream(uploadPaths, FileMode.Create);
                        fileDTO.FileContent = fileName;
                        await _fileBusiness.InsertFile(fileDTO);
                        file.CopyTo(stream);
                        stream.Dispose();
                    }
                    var mapperProduct = new ProductMapper();
                    var productDto = mapperProduct.MapperViewModelToDto(productViewModel);
                    _productBusiness.EditProduct(productDto);
                    return Redirect("/Admin/Product");
                } 
                ViewBag.Message = "Vui lòng chọn hình ảnh của sản phẩm";
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
            var menuDTOs = _menuBusiness.SelectAll();
            var dropdownList = new DropdownListItem();
            ViewData["ListMenu"] = new SelectList(dropdownList.DropdownListMenu(menuDTOs), "Value", "Text");
            return View();
        }
        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(List<IFormFile> uploadFiles,ProductViewModel productViewModel)
        {
            var menuDTOs = _menuBusiness.SelectAll();
            var dropdownList = new DropdownListItem();
            ViewData["ListMenu"] = new SelectList(dropdownList.DropdownListMenu(menuDTOs), "Value", "Text");
            if (!ModelState.IsValid) return View(productViewModel);
            if (productViewModel.IDCategoryProduct == "0")
            {
                ViewBag.Message = "Vui lòng chọn loại sản phẩm";
                return View(productViewModel);
            }
            try
            {
                if (uploadFiles.Count > 0)
                {
                    var mapperProduct = new ProductMapper();
                    var productDto = mapperProduct.MapperViewModelToDto(productViewModel);
                    var accountDTO = _accountBusiness.GetAccountByUsername(User.Identity.Name);
                    productDto.CreatedBy = accountDTO.ID;
                    long IDProduct = _productBusiness.CreateProduct(productDto);
                    var fileDTO = new FileDTO();
                    fileDTO.Type = 1;
                    fileDTO.CreatedBy = accountDTO.ID;
                    fileDTO.IDProduct = IDProduct;
                    fileDTO.Status = true;
                    foreach (var file in uploadFiles)
                    {
                        string fileName = file.FileName;
                        fileName = Path.GetFileName(fileName);
                        string uploadPaths = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//uploadFiles", fileName);
                        var stream = new FileStream(uploadPaths, FileMode.Create);
                        fileDTO.FileContent = fileName;
                        await _fileBusiness.InsertFile(fileDTO);
                        await file.CopyToAsync(stream);
                        stream.Dispose();
                    }
                    return Redirect("/Admin/Product");
                }
                ViewBag.Message = "Vui lòng chọn hình ảnh của sản phẩm";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Tạo sản phẩm thất bại";
            }
            return View(productViewModel);
        }
        [Area("Admin")]
        [HttpPost]
        public JsonResult GetProduct(string IDCategory)
        {
            try
            {
                var productDTO = _productBusiness.SelectByIDCategory(long.Parse(IDCategory));
                return Json(new
                {
                    status = true,
                    data = productDTO
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}
