using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Business.Interfaces;
using Shop.Entities.Enities;
using Shop.Mvc.Models;

namespace Shop.Mvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAccountBusiness _accountBusiness;
        private readonly IOrderBusiness _orderBusiness;
        private readonly IOrderDetailBusiness _orderDetailBusiness;
        private readonly IProductBusiness _productBusiness;
        public AccountController(UserManager<AppUser> userManager, IAccountBusiness accountBusiness,
            IOrderBusiness orderBusiness, IOrderDetailBusiness orderDetailBusiness, IProductBusiness productBusiness
            )
        {
            _userManager = userManager;
            _accountBusiness = accountBusiness;
            _orderBusiness = orderBusiness;
            _orderDetailBusiness = orderDetailBusiness;
            _productBusiness = productBusiness;
        }

        public IActionResult Index()
        {
            ViewBag.Active = "Information";
            var account = _accountBusiness.GetAccountByUsername(User.Identity.Name);
            var accountViewModel = new InformationAccount();
            accountViewModel.Address = account.Address;
            accountViewModel.Name = account.Name;
            accountViewModel.Phone = account.Phone;
            accountViewModel.Email= account.Email;
            accountViewModel.BirthDay = account.BirthDay;
            accountViewModel.Sex = (account.Sex == 1) ? "Nam" : (account.Sex == 2) ? "Nữ" : "Khác";
            return View(accountViewModel);
        }
        [HttpGet]
        [Route("/Account/OrderDetail/{id?}")]
        public IActionResult OrderDetail(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            try
            {
                var orderDetailDTO = _orderDetailBusiness.SelectByIDOrder(long.Parse(id));
                ViewBag.Active = "Order";
                ViewData["ListProduct"] = _productBusiness.SelectAll();
                return View(orderDetailDTO);
            }
            catch(Exception ex)
            {
                return Redirect("/404");
            }
        }
        public IActionResult Order()
        {
            try
            {
                ViewBag.Active = "Order";
                var user = _accountBusiness.GetAccountByUsername(HttpContext.User.Identity.Name);
                ViewBag.Address = user.Address;
                var orderDTO = _orderBusiness.SelectByIDAccount(user.ID);
                return View(orderDTO);
            }
            catch(Exception ex)
            {
                return Redirect("/404");
            }
        }
        [HttpGet]
        public IActionResult ChangeAccount()
        {
            ViewBag.Active = "Information";
            var account = _accountBusiness.GetAccountByUsername(User.Identity.Name);
            var accountViewModel = new InformationAccount();
            accountViewModel.Address = account.Address;
            accountViewModel.Name = account.Name;
            accountViewModel.Phone = account.Phone;
            accountViewModel.BirthDay = account.BirthDay;
            var gender = new List<Gender>
            {
                new Gender{ Name="Nam",Id = 1},
                new Gender{ Name="Nữ",Id = 2},
                new Gender{ Name="Khác",Id = 3}
            };
            ViewData["Gender"] = new SelectList(gender, "Id", "Name", account.Sex);
            return View(accountViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeAccount(InformationAccount accountViewModel)
        {
            var gender = new List<Gender>
            {
                new Gender{ Name="Nam",Id = 1},
                new Gender{ Name="Nữ",Id = 2},
                new Gender{ Name="Khác",Id = 3}
            };
            ViewData["Gender"] = new SelectList(gender, "Id", "Name", accountViewModel.Sex);
            if (!ModelState.IsValid) return View(accountViewModel);
            try
            {
                var accountDTO = _accountBusiness.GetAccountByUsername(User.Identity.Name);
                accountDTO.BirthDay = accountViewModel.BirthDay;
                accountDTO.Name = accountViewModel.Name;
                accountDTO.Sex = int.Parse(accountViewModel.Sex);
                accountDTO.Address = accountViewModel.Address;
                accountDTO.Phone = accountViewModel.Phone;
                var check = _accountBusiness.ChangeAccount(accountDTO);
                if (check == true)
                {
                    var appUser = await _userManager.FindByEmailAsync(accountDTO.Email);
                    appUser.BirthDay = accountViewModel.BirthDay;
                    appUser.Name = accountViewModel.Name;
                    appUser.Sex = int.Parse(accountViewModel.Sex);
                    appUser.Address = accountViewModel.Address;
                    appUser.PhoneNumber = accountViewModel.Phone;
                    var result = await _userManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Account");
                    }
                    else
                    {
                        ViewBag.Message = "Thay đổi thất bại";
                    }
                }
                else
                {
                    ViewBag.Message = "Số điện thoại đã tồn tại";
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Thay đổi thất bại";
            }
            return View(accountViewModel);

        }
        public IActionResult ChangePassword()
        {
            ViewBag.Active = "ChangePassword";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                if (resetPassword.Password == resetPassword.CurrentPassword)
                {
                    ViewBag.Message = "Mật khẩu mới phải khác mật khẩu cũ";
                    return View();
                }
                var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                var check = await _userManager.CheckPasswordAsync(user, resetPassword.CurrentPassword);
                if (check == false)
                {
                    ViewBag.Message = "Mật khẩu hiện tại không chính xác";
                    return View();
                }
                var result = await _userManager.ChangePasswordAsync(user,resetPassword.CurrentPassword,resetPassword.Password);
                if (result.Succeeded)
                {
                    _accountBusiness.ResetPassword(user.UserName,resetPassword.Password);
                    ViewBag.Success = "Thay đổi mật khẩu thành công";
                    return View();
                }
                else
                {
                    ViewBag.Message = "Mật khẩu mới không hợp lệ. Mật khẩu phải chứa 1 kí tự đặt biệt, chữ thường, chữ hoa và số";
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Thay đổi password thất bại";
            }
            return View();
        }
    }
}
