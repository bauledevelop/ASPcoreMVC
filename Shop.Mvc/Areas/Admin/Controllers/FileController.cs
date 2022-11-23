using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Mapper;
using Shop.Mvc.Areas.Admin.Models;
using Shop.Mvc.Commons.DropdownList;
using Shop.Mvc.Commons.Models;
using Shop.Mvc.Entensions;
using Shop.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    public class FileController : BaseController
    {
        private readonly IFileBusiness _fileBusiness;
        private readonly IProductBusiness _productBusiness;
        private readonly IAccountBusiness _accountBusiness;
        private readonly ICategoryProductBusiness _categoryProductBusiness;
        public FileController(IFileBusiness fileBusiness, IProductBusiness productBusiness,IAccountBusiness accountBusiness, ICategoryProductBusiness categoryProductBusiness)
        {
            _fileBusiness = fileBusiness;
            _productBusiness = productBusiness;
            _accountBusiness = accountBusiness;
            _categoryProductBusiness = categoryProductBusiness;
        }

        [Area("Admin")]
        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 1)
        {
            try
            {
                var model = _fileBusiness.SelectByQuantityItem(page, pageSize);
                var fileViewModels = new List<FileViewModel>();
                var mapperProduct = new ProductMapper();
                var mapperFile = new FileMapper();
                var mapperAccount = new AccountMapper();
                var pagination = new PaginationModel();
                var total = _productBusiness.GetTotal();
                var listProduct = _productBusiness.SelectAll();
                var listAccount = _accountBusiness.SelectAll();
                var categoryViewModel = new List<CategoryProductViewModel>();
                var accountViewModel = new List<AccountViewModel>();
                if (model != null)
                {
                    foreach(var item in listAccount)
                    {
                        accountViewModel.Add(mapperAccount.MapperDtoToViewModel(item));
                    }
                    foreach (var item in model)
                    {
                        fileViewModels.Add(mapperFile.MapperDtoToViewModel(item));
                    }
                    foreach (var item in fileViewModels)
                    {
                        item.IDProduct = listProduct.SingleOrDefault(child => child.ID == long.Parse(item.IDProduct)).Name;
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
                return View(fileViewModels);
            }
            catch(Exception ex)
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
                _fileBusiness.DeleteFIle(long.Parse(id));
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
        public IActionResult Create()
        {
            var dropdownList = new DropdownListItem();
            var listCategory = _categoryProductBusiness.SelectAllCategory();
            ViewData["TypeFile"] = new SelectList(dropdownList.DropdownListTypeFile(), "Value", "Text");
            ViewData["TypeCategory"] = new SelectList(dropdownList.DropdownListCategory(listCategory), "Value", "Text");
            return View();
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Create(FileViewModel fileViewModel)
        {
            var dropdownList = new DropdownListItem();
            var listCategory = _categoryProductBusiness.SelectAllCategory();
            ViewData["TypeFile"] = new SelectList(dropdownList.DropdownListTypeFileActive(fileViewModel.Type), "Value", "Text");
            ViewData["TypeCategory"] = new SelectList(dropdownList.DropdownListCategory(listCategory), "Value", "Text");
            if (!ModelState.IsValid) return View(fileViewModel);
            if (fileViewModel.IDProduct == "0")
            {
                ViewBag.Message = "Vui lòng chọn sản phẩm";
                return View(fileViewModel);
            }
            try
            {
                var mapperFile = new FileMapper();
                var fileDto = mapperFile.MapperViewModelToDto(fileViewModel);
                fileDto.CreatedBy = HttpContext.Session.Get<LoginModel>("LoginModel").ID;
                _fileBusiness.InsertFile(fileDto);
                return Redirect("/Admin/File");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Tạo dữ liệu thất bại";
            }
            return View(fileViewModel);
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var fileMapper = new FileMapper();
                var fileDTO = _fileBusiness.SelectById(long.Parse(id));
                var model = fileMapper.MapperDtoToViewModel(fileDTO);
                var dropdownList = new DropdownListItem();
                var productDto = _productBusiness.SelectById(fileDTO.IDProduct);
                var listProduct = _productBusiness.SelectByIDCategory(productDto.IDCategoryProduct);
                ViewData["TypeFile"] = new SelectList(dropdownList.DropdownListTypeFile(), "Value", "Text");
                ViewData["TypeProduct"] = new SelectList(dropdownList.DropdownListProductActive(listProduct, fileDTO.IDProduct), "Value", "Text");
                model.IDCategory = "check";
                return View(model);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Edit(FileViewModel fileViewModel)
        {
            var dropdownList = new DropdownListItem();
            var productDto = _productBusiness.SelectById(long.Parse(fileViewModel.IDProduct));
            var listProduct = _productBusiness.SelectByIDCategory(productDto.IDCategoryProduct);
            ViewData["TypeFile"] = new SelectList(dropdownList.DropdownListTypeFileActive(fileViewModel.Type), "Value", "Text");
            ViewData["TypeProduct"] = new SelectList(dropdownList.DropdownListProductActive(listProduct, long.Parse(fileViewModel.IDProduct)), "Value", "Text");
            if (!ModelState.IsValid) return View(fileViewModel);
            try
            {
                var mapperFile = new FileMapper();
                var fileDto = mapperFile.MapperViewModelToDto(fileViewModel);
                _fileBusiness.EditFile(fileDto);
                return Redirect("/Admin/File");
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Chỉnh sửa file không thành công";
            }
            return View(fileViewModel);
        }
        [Area("Admin")]
        [HttpPost]
        public JsonResult GetDataByIDProduct(string IDProduct)
        {
            if (string.IsNullOrEmpty(IDProduct)) throw new ArgumentNullException(nameof(IDProduct));
            try
            {
                var fileDTOs = _fileBusiness.SelectByIDProduct(long.Parse(IDProduct));                
                if (fileDTOs.Count() > 0) {
                    return Json(new
                    {
                        status = true,
                        data = fileDTOs
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = false
                    });
                }
                
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}
