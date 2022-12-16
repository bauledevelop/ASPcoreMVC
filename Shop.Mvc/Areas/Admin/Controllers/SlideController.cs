using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Implements;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Models;
using System.Data;
using X.PagedList;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SlideController : Controller
    {
        private readonly ISlideBusiness _slideBusiness;
        public SlideController(ISlideBusiness slideBusiness)
        {
            _slideBusiness = slideBusiness;
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Index(int page=1,int pageSize=5)
        {
            try
            {
                var model = _slideBusiness.SelectByQuantityItem(page, pageSize);
               
                ViewData["Pagination"] = _slideBusiness.SelectAll().ToPagedList(page, pageSize);
                return View(model);
            }
            catch(Exception ex)
            {
                return Redirect("/404");
            }
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Area("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(List<IFormFile> uploadFiles)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                if (uploadFiles.Count > 0)
                {
                    var slideDTO = new SlideDTO();
                    foreach (var file in uploadFiles)
                    {
                        string fileName = file.FileName;
                        fileName = Path.GetFileName(fileName);
                        string uploadPaths = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//slide", fileName);

                        var stream = new FileStream(uploadPaths, FileMode.Create);
                        slideDTO.Content = fileName;
                        _slideBusiness.InsertSlide(slideDTO);
                        await file.CopyToAsync(stream);
                        stream.Dispose();
                    }
                    return Redirect("/Admin/Slide");
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Tạo slide thất bại";
            }
            return View();
        }
        [HttpPost]
        [Area("Admin")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var slide = _slideBusiness.SelectByID(long.Parse(id));
                var ID = long.Parse(id);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//slide", slide.Content);
                System.IO.File.Delete(path);
                _slideBusiness.DeleteSlide(ID);
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
    }
}
