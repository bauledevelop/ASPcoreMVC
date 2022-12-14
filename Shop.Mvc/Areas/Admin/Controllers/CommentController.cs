using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Business.Interfaces;
using Shop.Mvc.Areas.Admin.Mapper;
using Shop.Mvc.Areas.Admin.Models;
using Shop.Mvc.Commons.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CommentController : Controller
    {
        private readonly ICommentBusiness _commentBusiness;
        private readonly IAccountBusiness _accountBusiness;
        public CommentController(ICommentBusiness commonBusiness,IAccountBusiness accountBusiness,IProductBusiness productBusiness)
        {
            _commentBusiness = commonBusiness;
            _accountBusiness = accountBusiness;
        }

        [Area("Admin")]
        [HttpGet]
        public IActionResult Index(int page=1,int pageSize=1)
        {
            try
            {
                var model = _commentBusiness.SelectByQuantityItem(page, pageSize);
                var commentViewModel = new List<CommentViewModel>();
                var mapperComment = new CommentMapper();
                var mapperAccount = new AccountMapper();
                var listAccount = _accountBusiness.SelectAll();
                var accountViewModel = new List<AccountViewModel>();
                if (model != null)
                {
                    foreach (var item in listAccount)
                    {
                        accountViewModel.Add(mapperAccount.MapperDtoToViewModel(item));
                    }
                    foreach (var item in model)
                    {
                        commentViewModel.Add(mapperComment.MapperDtoToViewModel(item));
                    }
                    
                    ViewData["ListAccount"] = accountViewModel;
                    ViewData["Pagination"] = _commentBusiness.SelectAll().ToPagedList(page,pageSize);
                }
                return View(commentViewModel);
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
                _commentBusiness.DeleteComment(long.Parse(id));
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
