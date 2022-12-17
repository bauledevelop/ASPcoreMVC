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
        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var fileDTO = _fileBusiness.SelectById(long.Parse(id));
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//uploadFiles", fileDTO.FileContent);
                System.IO.File.Delete(path);
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
