using Microsoft.AspNetCore.Authorization;
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
using X.PagedList;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FileController : Controller
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
                    ViewData["ListAccount"] = accountViewModel;
                    ViewData["Pagination"] = _fileBusiness.SelectAll().ToPagedList(page, pageSize);
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
        public IActionResult Create(FileViewModel fileViewModel,List<IFormFile> uploadFiles)
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
                var accountDTO = _accountBusiness.GetAccountByUsername(User.Identity.Name);
                fileDto.CreatedBy = accountDTO.ID;
                if (uploadFiles.Count > 0)
                {
                    foreach (var file in uploadFiles)
                    {
                        string fileName = file.FileName;
                        fileName = Path.GetFileName(fileName);
                        string uploadPaths = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//uploadFiles", fileName);

                        var stream = new FileStream(uploadPaths, FileMode.Create);
                        fileDto.FileContent = "/uploadFiles/" + fileName;
                        _fileBusiness.InsertFile(fileDto);
                        file.CopyToAsync(stream);

                    }
                    return Redirect("/Admin/File");
                }
                else
                {
                    ViewBag.Message = "Vui lòng thêm file";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Tạo dữ liệu thất bại";
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
