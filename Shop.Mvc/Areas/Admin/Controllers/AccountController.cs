using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
    public class AccountController : BaseController
    {
        private readonly IAccountBusiness _accountBusiness;
        public AccountController(IAccountBusiness accountBusiness)
        {
            _accountBusiness = accountBusiness;
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Index(int page=1,int pageSize=1)
        {
            try
            {
                var model = _accountBusiness.SelectByQuantityItem(page, pageSize);
                var accountViewModels = new List<AccountViewModel>();
                var mapperAccount = new AccountMapper();
                var pagination = new PaginationModel();
                var total = _accountBusiness.GetTotal();
                if (model != null)
                {
                    foreach (var item in model)
                    {
                        accountViewModels.Add(mapperAccount.MapperDtoToViewModel(item));
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
                    ViewData["Pagination"] = pagination;
                }
                return View(accountViewModels);
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
            _accountBusiness.DeleteAccount(long.Parse(id));
            return Json(new
            {
                status = true
            });
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var dropDownList = new DropdownListItem();
            ViewData["TypeSex"] = new SelectList(dropDownList.DropdownListTypeSex(), "Value", "Text");
            ViewData["TypeAccount"] = new SelectList(dropDownList.DropdownListTypeAccount(), "Value", "Text");
            return View();
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Create(AccountViewModel accountViewModel)
        {
            if (!ModelState.IsValid) return View(accountViewModel);
            try
            {
                var accountDto = new AccountDTO();
                var mapperAccount = new AccountMapper();
                var check = 0;
                accountDto = mapperAccount.MapperViewModelToDTO(accountViewModel);
                check = _accountBusiness.InsertAccount(accountDto);
                if (check == 1)
                {
                    return Redirect("/Admin/Account");
                }
                else if (check == 2)
                {
                    ViewBag.Message = "Email đã tồn tại";
                }
                else
                    ViewBag.Message = "Số điện thoại đã tồn tại";
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Thêm tài khoản không thành công";
            }
            return View(accountViewModel);
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var accoutDto = new AccountDTO();
                var model = new AccountViewModel();
                var mapperAccount = new AccountMapper();
                var dropDownList = new DropdownListItem();
                accoutDto = _accountBusiness.GetAccountById(long.Parse(id));
                model = mapperAccount.MapperDtoToViewModel(accoutDto);
                ViewData["TypeSex"] = new SelectList(dropDownList.DropdownListTypeSexActive(model.Sex), "Value", "Text");
                ViewData["TypeAccount"] = new SelectList(dropDownList.DropdownListTypeAccountActive(model.AccountType), "Value", "Text");
                return View(model);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
        [Area("Admin")]
        [HttpPost]
        public IActionResult Edit(AccountViewModel accountViewModel)
        {
            var dropDownList = new DropdownListItem();
            ViewData["TypeSex"] = new SelectList(dropDownList.DropdownListTypeSexActive(accountViewModel.Sex), "Value", "Text");
            ViewData["TypeAccount"] = new SelectList(dropDownList.DropdownListTypeAccountActive(accountViewModel.AccountType), "Value", "Text");
            if (!ModelState.IsValid) return View(accountViewModel);
            var accountDto = new AccountDTO();
            var mapperAccount = new AccountMapper();
            accountDto = mapperAccount.MapperViewModelToDTO(accountViewModel);
            var check = _accountBusiness.EditAccount(accountDto);
            if (check)
            {
                return Redirect("/Admin/Account");
            }
            else
            {
                ViewBag.Message = "Số điện thoại đã tồn tại";
            }
            return View(accountViewModel);
        }
    }
}
