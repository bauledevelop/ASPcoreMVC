using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Mvc.Areas.Admin.Mapper;
using Shop.Mvc.Areas.Admin.Models;
using Shop.Mvc.Commons.DropdownList;
using X.PagedList;

namespace Shop.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountController : Controller
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
                var model = _accountBusiness.SelectByQuantityItem(page,pageSize);
                var accountViewModels = new List<AccountViewModel>();
                var mapperAccount = new AccountMapper();
                if (model != null)
                {
                    foreach (var item in model)
                    {
                        accountViewModels.Add(mapperAccount.MapperDtoToViewModel(item));
                    }
                    ViewData["Pagination"] = _accountBusiness.SelectAll().ToPagedList(page,pageSize);
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
