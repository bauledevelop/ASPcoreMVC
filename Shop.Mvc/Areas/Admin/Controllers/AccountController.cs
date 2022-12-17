using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Business.Interfaces;
using Shop.Common.DTO;
using Shop.Entities.Enities;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        public AccountController(IAccountBusiness accountBusiness, UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _accountBusiness = accountBusiness;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }
        [Area("Admin")]
        [HttpGet]
        public IActionResult Index(int page=1,int pageSize=5)
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
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var accountDTO = _accountBusiness.GetAccountById(long.Parse(id));
                var user = await _userManager.FindByEmailAsync(accountDTO.Email);
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    _accountBusiness.DeleteAccount(long.Parse(id));
                    return Json(new
                    {
                        status = true
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
        public async Task<IActionResult> Create(AccountViewModel accountViewModel)
        {
            var dropDownList = new DropdownListItem();
            ViewData["TypeSex"] = new SelectList(dropDownList.DropdownListTypeSexActive(accountViewModel.AccountType), "Value", "Text");
            ViewData["TypeAccount"] = new SelectList(dropDownList.DropdownListTypeAccountActive(accountViewModel.Sex), "Value", "Text");
            if (!ModelState.IsValid) return View(accountViewModel);
            try
            {
                var accountDto = new AccountDTO();
                var mapperAccount = new AccountMapper();
                var check = 0;
                accountDto = mapperAccount.MapperViewModelToDTO(accountViewModel);
                var appUser = new AppUser
                {
                    UserName = accountViewModel.Username,
                    Email = accountViewModel.Email,
                    PhoneNumber = accountViewModel.Phone,
                    BirthDay = accountViewModel.BirthDay,
                    Name = accountViewModel.Name,
                    Sex = int.Parse(accountViewModel.Sex),
                    Address = accountViewModel.Address,
                    AccountType = int.Parse(accountViewModel.AccountType),
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(appUser, accountViewModel.Password);
                if (result.Succeeded)
                {
                    var role = await _userManager.AddToRoleAsync(appUser, (appUser.AccountType == 1) ? "Administrator" : "User");
                    _accountBusiness.InsertAccount(accountDto);
                    return Redirect("/Admin/Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            catch (Exception ex)
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
        public async Task<IActionResult> Edit(AccountViewModel accountViewModel)
        {
            var dropDownList = new DropdownListItem();
            ViewData["TypeSex"] = new SelectList(dropDownList.DropdownListTypeSexActive(accountViewModel.Sex), "Value", "Text");
            ViewData["TypeAccount"] = new SelectList(dropDownList.DropdownListTypeAccountActive(accountViewModel.AccountType), "Value", "Text");
            if (!ModelState.IsValid) return View(accountViewModel);
            try
            {
               
                var accountDto = new AccountDTO();
                var mapperAccount = new AccountMapper();
                var currentAccount = _accountBusiness.GetAccountById(accountViewModel.ID);
                var appUser = await _userManager.FindByEmailAsync(accountViewModel.Email);
                var changePassword = await _userManager.ChangePasswordAsync(appUser, currentAccount.Password, accountViewModel.Password);
                if (changePassword.Succeeded)
                {
                    var deleteRole = await _userManager.RemoveFromRoleAsync(appUser, (appUser.AccountType == 1) ? "Administrator" : "User");
                    if (deleteRole.Succeeded)
                    {
                        appUser.PasswordHash = _passwordHasher.HashPassword(appUser, accountViewModel.Password);
                        appUser.Name = accountViewModel.Name;
                        appUser.Sex = int.Parse(accountViewModel.Sex);
                        appUser.Address = accountViewModel.Address;
                        appUser.PhoneNumber = accountViewModel.Phone;
                        appUser.AccountType = int.Parse(accountViewModel.AccountType);
                        var result = await _userManager.UpdateAsync(appUser);
                        if (result.Succeeded)
                        {
                            var changeRole = await _userManager.AddToRoleAsync(appUser, (appUser.AccountType == 1) ? "Administrator" : "User");
                            if (changeRole.Succeeded)
                            {
                                accountDto = mapperAccount.MapperViewModelToDTO(accountViewModel);
                                _accountBusiness.EditAccount(accountDto);
                                return Redirect("/Admin/Account");
                            }
                        }
                        else
                        {
                            foreach (var error in deleteRole.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }

                    }
                    else
                    {
                        ViewBag.Message = "Thay đổi tài khoản thất bại";
                    }

                }
                else
                {
                    foreach (var error in changePassword.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Thay đổi thất bại";
            }
            return View(accountViewModel);
        }
    }
}
